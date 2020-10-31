using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FinalProject;

namespace FinalProject
{
    public partial class Store : System.Web.UI.Page
    {
        string connString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;


        SqlUtil myUtils = new SqlUtil();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string queryString = "SELECT Product.productID, Product.productName, Product.productDescription, Product.productPrice, Product.productQuantity, Company.companyName, Category.categoryName FROM Product LEFT JOIN ProductCompany ON Product.productID = ProductCompany.productID LEFT JOIN Company ON ProductCompany.companyID = Company.companyID LEFT JOIN ProductCategory ON product.productID = ProductCategory.productID LEFT JOIN Category ON ProductCategory.categoryID = Category.categoryID";
                BindGridView(queryString);
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        //select statement for company columns
        protected DataTable GetCompanies()
        {
            return myUtils.GetDataTables("Select companyID, companyName from Company");
        }

        //select statement for category columns 
        protected DataTable GetCategories()
        {
            return myUtils.GetDataTables("Select categoryID, categoryName from Category");

        }

       //method to excute query and return a result if successful
        protected int ExecuteNonQueryReturn(string queryString, SqlParameter[] prms)
        {
            int res = 0;
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.Parameters.AddRange(prms);

                res = Convert.ToInt32(command.ExecuteNonQuery());
            }
            return res;
        }

        //method to excute scalar to ruturn a result 
        protected int ExecuteScalarReturn(string queryString)
        {
            int res = 0;
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                res = Convert.ToInt32(command.ExecuteScalar());
            }
            return res;
        }

        

        // Using the query, go to our database to get the returned values
        // then return those value into a DataTable. Once done, use that DataTable
        // as a DataSource for our GridView and bind to the grid view
        protected void BindGridView(string sqlQuery)
        {
            DataTable dt = myUtils.GetDataTables(sqlQuery);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }


        // OVERLOADED METHOD
        // Using the query and give params, go to our database to get the returned values
        // then return those value into a DataTable. Once done, use that DataTable
        // as a DataSource for our GridView and bind to the grid view
        protected void BindGridView(string sqlQuery, SqlParameter[] param)
        {
            DataTable dt = myUtils.GetDataTables(sqlQuery, param);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }




        protected void btnSearch_Click(object sender, EventArgs e)
        {
            

            string searchQuery = "SELECT Product.productID, Product.productName, Product.productDescription, Product.productPrice, Product.productQuantity, Company.companyName, Category.categoryName FROM Product LEFT JOIN ProductCompany ON Product.productID = ProductCompany.productID LEFT JOIN Company ON ProductCompany.companyID = Company.companyID LEFT JOIN ProductCategory ON product.productID = ProductCategory.productID LEFT JOIN Category ON ProductCategory.categoryID = Category.categoryID";

            switch (ddlSearchType.SelectedValue) {
                case "Product":
                    searchQuery += " WHERE Product.productName LIKE '%' + @searchText +'%'";
                    break;
                case "Company":
                    searchQuery += " WHERE Company.companyName LIKE '%' + @searchText +'%'";
                    break;
                case "Category":
                    searchQuery += " WHERE Category.categoryName LIKE '%' + @searchText +'%'";
                    break;
                }
             
            // Remove current items from our grid view 
            GridView1.DataSource = null;

            // Make SQLParam array, as it protects from SQL Injections
            SqlParameter[] sqlParamsSearch = new SqlParameter[] {
                    new SqlParameter("@searchText", txtSearch.Text),

            };

            // Add items that meet the criteria to our grid view
            BindGridView(searchQuery, sqlParamsSearch);



        }

        protected void btnResetSearch_Click(object sender, EventArgs e)
        {
            // Refresh page
            Response.Redirect(Request.RawUrl,true);
        }


        // Add items to the cart
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)GridView1.Rows[0].Cells[0].FindControl("GVQuestionTextBox");
            string Ques = txt.Text;//This will be desired one
        }


        protected bool CheckQuantity (int storeQuantity, int requestQuantity)
        {

            // Check if num is negative
            if (requestQuantity <= 0)
            {
                lblErrorMsg.Text = "Error: Invalid number; must be a positive integer";
                return false;
                
            }
            // Check if requested number is more than store's stock
            else if (requestQuantity > storeQuantity)
            {
                lblErrorMsg.Text = "Error: Requested quantity is greater than current stock";
                return false;
                
            }


            return true;

        }


        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            lblErrorMsg.Text = "";
            int index = Convert.ToInt32(e.CommandArgument);
            int productID = Convert.ToInt32(GridView1.DataKeys[index].Values[0]);
            int storeQuantity = Convert.ToInt32(GridView1.Rows[index].Cells[3].Text);

            TextBox txtReqQuantity = (TextBox)GridView1.Rows[index].FindControl("txtOrderQuantity");
            int requestedQuantity;

            // Check to confirm the number is a valid int
            if (int.TryParse(txtReqQuantity.Text, out requestedQuantity))
            {
                // If number is correct and we have the stock, add it to the cart
                if (CheckQuantity(storeQuantity, requestedQuantity))
                {
                    manageSessionProducts(productID, requestedQuantity);
                    //Page.Response.Redirect("./Cart.aspx");
                }
                
            }
        }

               
        protected void manageSessionProducts(int productID, int productQuantity)
        {
            Dictionary<int, int> sessionProducts = new Dictionary<int, int>();


            // Check if there is an existing session with products
            if (Session["products"] == null)
            {
                sessionProducts.Add(productID, productQuantity);
            }
            else
            {
                // Pull existing session products
                sessionProducts = Session["products"] as Dictionary<int, int>;

                // Check existing list for product in case of quantity change
                // If yes, update quantity
                if (sessionProducts.ContainsKey(productID))
                {
                    sessionProducts[productID] = productQuantity;
                }
                // If no, add to list of products
                else
                {
                    sessionProducts.Add(productID, productQuantity);
                }
            }


            // Update products session
            Session["products"] = sessionProducts;
        }

    }

}