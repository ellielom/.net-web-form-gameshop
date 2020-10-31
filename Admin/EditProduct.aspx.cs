using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FinalProject;

/// <summary>
/// Principal Author: Ellie Lombardi
/// Purpose: Since the product information is shared on three tables, 
/// we have to do 3 SQL update statements which cannot be done through the built-in gridview update.
/// Code validates user's input and then runs the update on all three tables.
/// </summary>
/// 

namespace FinalProject.Admin
{
    public partial class EditProduct : System.Web.UI.Page
    {
        string connString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        SqlUtil myUtils = new SqlUtil();

        


        // Since the prodID is used throughout various methods, we'll declare it at the class level
        int prodID;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["isAdmin"] == null || Session["isAdmin"].ToString().Equals("false"))
            {
                Page.Response.Redirect("../Store.aspx");
            }

            bool isProdIDInSession = false;

            // Check to see if a product has been properly selected and is stored in the session
            if (Session["prodID"] != null)
            {
                hdnProdID.Value = Session["prodID"].ToString();
                Session["prodID"] = null;
                isProdIDInSession = true;
            }
            else
            {
                lblError.Text = "Product not found";
            }

            prodID = Convert.ToInt32(hdnProdID.Value);

            // On page load, load the information for the selected product (via prodID and also
            // populate the category & company drop down lists so the user can select from them
            if (!Page.IsPostBack && isProdIDInSession)
            {
                PopulateDropDownList(ddlCompany, GetCompanies(), "companyName", "companyID");
                PopulateDropDownList(ddlCategory, GetCategories(), "categoryName", "categoryID");

                LoadProduct(prodID);
            }

            

        }

        protected void PopulateDropDownList(DropDownList ddl, DataTable dt, string text, string value)
        {
            // Generic method that allows us to populate a drop down list given a datatable
            ddl.DataSource = dt;
            ddl.DataTextField = text;
            ddl.DataValueField = value;
            ddl.DataBind();

        }


        protected DataTable GetCompanies()
        {
            // Creates a DataTable for Companies
            return myUtils.GetDataTables("Select companyID, companyName from Company");
        }

        protected DataTable GetCategories()
        {
            // Creates a DataTable for Categories
            return myUtils.GetDataTables("Select categoryID, categoryName from Category");

        }


        protected void LoadProduct(int prodID)
        {

            // Load product information
            string productQuery = "SELECT productName, productDescription, productPrice, productQuantity FROM Product " +
                "WHERE productID = " + prodID;
 
            // Make a SQL Connection that will take in the above query and return all the 
            // current information for the product. Then read that information and insert
            // it into our web form so that we have the current values available to the user
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand myCommand = new SqlCommand(productQuery, connection);
                connection.Open();
                SqlDataReader sqlReader = myCommand.ExecuteReader();
                while (sqlReader.Read())
                {
                    txtName.Text = sqlReader["productName"].ToString();
                    txtDescription.Text = sqlReader["productDescription"].ToString();
                    txtPrice.Text = sqlReader["productPrice"].ToString();
                    txtQuantity.Text = sqlReader["productQuantity"].ToString();
                }
            }


            // Load Category Value
            string categoryQuery = "SELECT c.categoryID FROM Category c LEFT JOIN " +
                "ProductCategory p ON c.categoryID = p.categoryID WHERE p.productID = " + prodID;

            //int categoryID = 0;

            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand myCommand = new SqlCommand(categoryQuery, connection);
                connection.Open();
                ddlCategory.SelectedValue = myCommand.ExecuteScalar().ToString();

            }



            // Load Company Value
            string companyQuery = "SELECT c.companyID FROM Company c LEFT JOIN " +
                "ProductCompany p ON c.companyID = p.companyID WHERE p.productID = " + prodID;

            int companyID = 0;

            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand myCommand = new SqlCommand(companyQuery, connection);
                connection.Open();
               
                var res = myCommand.ExecuteScalar();
                companyID = Convert.ToInt32(res);
            }

            ddlCompany.SelectedValue = companyID.ToString();
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Instaniate necessary variables 
            int res = 0;
            int quantity;
            float price;

            // Check if the price & quantity are numbers 
            if (int.TryParse(txtQuantity.Text, out quantity) && float.TryParse(txtPrice.Text, out price))
            {

            ////// Update Products Table
                string updateProduct = "UPDATE Product SET productName = @productName, productDescription = @productDescription, " +
                    "productPrice = @productPrice, productQuantity = @productQuantity WHERE productID = @prodID";

                // Make SQLParam array, as it protects from SQL Injections
                SqlParameter[] sqlParamsProduct = new SqlParameter[] {
                    new SqlParameter("@productName", txtName.Text),
                    new SqlParameter("@productDescription", txtDescription.Text),
                    new SqlParameter("@productPrice", Convert.ToDecimal(txtPrice.Text)),
                    new SqlParameter("@productQuantity", Convert.ToInt32(txtQuantity.Text)),
                    new SqlParameter("@prodID", prodID),
            };
                // Execute insert and return success int
                res += myUtils.ExecuteNonQueryReturn(updateProduct, sqlParamsProduct);




                ////// Update ProductCategory Table
                string updateProductCategory = "UPDATE ProductCategory SET categoryID = @categoryID WHERE productID = @prodID";

                // Make SQLParam array 
                SqlParameter[] sqlParamsProductCategory = new SqlParameter[] {
                    new SqlParameter("@categoryID", ddlCategory.SelectedValue),
                    new SqlParameter("@prodID", prodID),
                };
                // Execute insert and return success int
                res += myUtils.ExecuteNonQueryReturn(updateProductCategory, sqlParamsProductCategory);





                ////// Update ProductCompany Table
                string updateProductCompany = "UPDATE ProductCompany SET companyID = @companyID WHERE productID = @prodID";

                // Make SQLParam array 
                SqlParameter[] sqlParamsProductCompany = new SqlParameter[] {
                    new SqlParameter("@companyID", ddlCompany.SelectedValue),
                    new SqlParameter("@prodID", prodID),
                };
                // Execute insert and return success int
                res += myUtils.ExecuteNonQueryReturn(updateProductCompany, sqlParamsProductCompany);


                // If all are successful, redirect back to the product page
                if (res > 0)
                {
                    // redirect
                    //Page.Response.Redirect(Page.Request.Url.ToString(), true);
                    //lblError.Text = "Updated successfully";

                    Page.Response.Redirect("./ManageProducts.aspx", true);
                }

            }
            else
            // If quantity and price aren't valid numbers
            {
                lblError.Text = "Error: Please enter a correct price and quantity";
            }

            
            

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect("./ManageProducts.aspx", true);
        }
    }
}   