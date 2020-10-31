using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        string connString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["isAdmin"] == null || Session["isAdmin"].ToString().Equals("false"))
            {
                Page.Response.Redirect("../Store.aspx");
            }

            if (!Page.IsPostBack)
            {
                BindGridView();
                PopulateDropDownList(ddlCompany, GetCompanies(), "companyName", "companyID");
                PopulateDropDownList(ddlCategory, GetCategories(), "categoryName", "categoryID");
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //populate drop down list 
        protected void PopulateDropDownList(DropDownList ddl, DataTable dt, string text, string value)
        {
            ddl.DataSource = dt;
            ddl.DataTextField = text;
            ddl.DataValueField = value;
            ddl.DataBind();
            ddl.Items.Insert(0, "Please select");

        }

        //fills all selected tables with data 
        protected DataTable GetDataTables(string queryString)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                DataSet ds = new DataSet();
                (new SqlDataAdapter(command)).Fill(ds);

                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            return dt;
        }

        //select statement for company columns
        protected DataTable GetCompanies()
        {
            return GetDataTables("Select companyID, companyName from Company");
        }

        //select statement for category columns 
        protected DataTable GetCategories()
        {
            return GetDataTables("Select categoryID, categoryName from Category");

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

        //button to add text box values into tables in the database 
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //sql statment variables
            string selectProductquery = "Select max(productID) from Product";
            string productQuery = "Insert into Product (productName, productDescription, productPrice, productQuantity) " +
                "values (@pName, @pDesc, @pPrice, @pQuantity)";
            string companyQuery = "Insert into ProductCompany (companyID, productID) " +
                "values (@companyID, @productID)";
            string categoryQuery = "Insert into ProductCategory (categoryID, productID) " +
                "values (@categoryID, @productID)";

            int prodQuantity = 0;
            double price = 0;

            if (int.TryParse(txtQuantity.Text, out prodQuantity) && double.TryParse(txtPrice.Text, out price))
            {
                //sql array to match the textbox values inserted
                SqlParameter[] sqlParams = new SqlParameter[] {
            new SqlParameter("@pName", txtName.Text),
            new SqlParameter("@pDesc", txtDesc.Text),
            new SqlParameter("@pPrice", txtPrice.Text),
            new SqlParameter("@pQuantity", txtQuantity.Text)
            };
                //method to execute the productQuery insert command with values from the array
                ExecuteNonQueryReturn(productQuery, sqlParams);

                //prodID is set to select the most recent added product 
                int prodID = ExecuteScalarReturn(selectProductquery);

                //sql array for the bridge table ProductCompany
                SqlParameter[] sqlParams2 = new SqlParameter[]
                {
            new SqlParameter("@companyID", ddlCompany.SelectedValue),
            new SqlParameter("@productID", prodID)
                };

                //sql array for the bridge table ProductCategory 
                SqlParameter[] sqlParams3 = new SqlParameter[]
                {
            new SqlParameter("@categoryID", ddlCategory.SelectedValue),
            new SqlParameter("@productID", prodID)
                };
                ExecuteNonQueryReturn(companyQuery, sqlParams2);
                ExecuteNonQueryReturn(categoryQuery, sqlParams3);

                Page.Response.Redirect(Page.Request.Url.ToString(), true);


                
            }
            else
            {
                lblErrorMessage.Text = "Error: Please enter a correct price and quantity";
            }


            
        }//end of add button 

        //methof to bing GridView
        protected void BindGridView()
        {
            string queryString = "SELECT Product.productID, Product.productName, Product.productDescription, Product.productPrice, Product.productQuantity, Company.companyName, Category.categoryName FROM Product 	LEFT JOIN ProductCompany ON Product.productID = ProductCompany.productID LEFT JOIN Company ON ProductCompany.companyID = Company.companyID LEFT JOIN ProductCategory ON product.productID = ProductCategory.productID LEFT JOIN Category ON ProductCategory.categoryID = Category.categoryID";

            DataTable dt = GetDataTables(queryString);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        //method for delete and edit button 
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int productID = Convert.ToInt32(GridView1.Rows[index].Cells[0].Text);

            if (e.CommandName.Equals("delete"))
            {
                
                string productQuery = "Delete from Product where productID = @productID";
                string comProductQuery = "delete from productCompany where productID = @productID";
                string catProductQuery = "delete from productCategory where productID = @productID";
                //int productID = Convert.ToInt32(GridView1.Rows[index].Cells[0].Text);

                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@productID", productID)
                };

                SqlParameter[] sqlParams2 = new SqlParameter[] 
                {
                    new SqlParameter("@productID", productID)
                };

                SqlParameter[] sqlParams3 = new SqlParameter[] 
                {
                    new SqlParameter("@productID", productID)
                };
                ExecuteNonQueryReturn(productQuery, sqlParams);
                ExecuteNonQueryReturn(comProductQuery, sqlParams2);
                ExecuteNonQueryReturn(catProductQuery, sqlParams3);

                // GridViewRow row = (GridViewRow)GridView1.Rows[];
                //SqlCommand cmd = new SqlCommand("Delete From Product where productID = @productID");

            Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
            else if(e.CommandName.Equals("edit"))
            {
                Session["prodID"] = productID;
                Page.Response.Redirect("./EditProduct.aspx", true);
            }
        }
    }

}