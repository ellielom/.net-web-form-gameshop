using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FinalProject;

namespace FinalProject
{

    /// <summary>
    /// Author: Ellie Lombardi
    /// Purpose: Allow the user to check out with their given (if any) selected products
    /// </summary>
    public partial class Cart : System.Web.UI.Page
    {
        SqlUtil myUtils = new SqlUtil();
        Dictionary<int, int> myProducts;

        protected void Page_Load(object sender, EventArgs e)
        {
            // If the user is not logged in, disable order button
            /*
            if (Session["userID"] == null)
            {
                btnOrder.Enabled = false;
                lblLogin.Visible = true;
            }
            */

            if (!IsPostBack) {
                if (Session["products"] == null) {
                    lblErrorMsg.Text = "Cart is empty";
                }
                else
                {
                    // Check dictionary has at least 1 record
                    myProducts = Session["products"] as Dictionary<int, int>;
                    
                    // If no, display error msg
                    if (myProducts.Count == 0)
                    {
                        lblErrorMsg.Text = "Cart is empty";
                    }
                    
                    // If yes, get product ids from dictionary and use them to pull that information from our database
                    else
                    {
                        PrepareGridViewWithSessionProducts(myProducts);
                    }
                }
            }
        }

        // Using the product IDs listed in the dictionary, pull the related product information from a database and display in a gridview
        protected void PrepareGridViewWithSessionProducts(Dictionary<int, int> productDictionary)
        {
            string productIDs = "";

            // Loop through dictionary to pull all productIDs
            foreach (int product in productDictionary.Keys)
            {
                productIDs += (product + ",");
            }

            // Remove trailing comma
            productIDs = productIDs.Substring(0, productIDs.Length - 1);


            // SQL Query that pulls info for all the products in our session
            string sqlQuery = "SELECT Product.productID, Product.productName, Product.productDescription, Product.productPrice, Product.productQuantity, Company.companyName, Category.categoryName FROM Product LEFT JOIN ProductCompany ON Product.productID = ProductCompany.productID LEFT JOIN Company ON ProductCompany.companyID = Company.companyID LEFT JOIN ProductCategory ON product.productID = ProductCategory.productID LEFT JOIN Category ON ProductCategory.categoryID = Category.categoryID WHERE Product.productID IN (";
            




            // DataBind using our handy dandy BindGridView method
            BindGridView(sqlQuery, myProducts);
            LoadTextBoxes(myProducts);
            PrepareProductTotal();
        }


        protected void PrepareProductTotal()
        {
            double subtotal = 0;

            foreach(GridViewRow row in GridView1.Rows)
            {
                double price = Convert.ToDouble(row.Cells[2].Text);
                TextBox txtReqQuantity = (TextBox)GridView1.Rows[row.RowIndex].FindControl("txtOrderQuantity");
                int quantity = Convert.ToInt32(txtReqQuantity.Text);
                subtotal += price * quantity;
            }

            double taxes = subtotal * 0.13;
            double total = subtotal + taxes;

            lblSubtotal.Text = subtotal.ToString("C");
            lblTax.Text = taxes.ToString("C");
            lblTotal.Text = total.ToString("C");


        }


        // Using the query, go to our database to get the returned values
        // then return those value into a DataTable. Once done, use that DataTable
        // as a DataSource for our GridView and bind to the grid view
        protected void BindGridView(string sqlQuery, Dictionary<int,int> prodDictionary)
        {
            DataTable dt = myUtils.GetDataTables(sqlQuery, prodDictionary);
            GridView1.DataSource = dt;
            GridView1.DataBind();

        }

        // Load the textboxes for each item so that it displays the user's current requested quantity
        protected void LoadTextBoxes(Dictionary<int, int> prodDictionary)
        {
            foreach(GridViewRow row in GridView1.Rows)
            {
                // Get the product ID, which we will use as a key in our dictionary
                string dataKeyValue = GridView1.DataKeys[row.RowIndex].Values[0].ToString();
                // Get the textbox from the GridView
                TextBox txtReqQuantity = (TextBox)GridView1.Rows[row.RowIndex].FindControl("txtOrderQuantity");

                // From the session's dictionary, get the quantity value
                myProducts = Session["products"] as Dictionary<int, int>;
                txtReqQuantity.Text = myProducts[Convert.ToInt32(dataKeyValue)].ToString();
               
            }
        }


        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int productID = Convert.ToInt32(GridView1.DataKeys[index].Values[0].ToString());
            myProducts = Session["products"] as Dictionary<int, int>;

            if (e.CommandName.Equals("RemoveItemFromCart"))
            {               
                myProducts.Remove(productID);
                Session["products"] = myProducts;
                Page.Response.Redirect("./Cart.aspx");
            }
            else if (e.CommandName.Equals("UpdateItemInCart"))
            {
                TextBox txtReqQuantity = (TextBox)GridView1.Rows[index].FindControl("txtOrderQuantity");


                int reqQuantity = Convert.ToInt32(txtReqQuantity.Text);
                int storeQuantity = Convert.ToInt32(GridView1.Rows[index].Cells[3].Text);

                // Three possible flows
                // 1. They set quantity to 0
                if (reqQuantity == 0)
                {
                    //remove from list
                    myProducts.Remove(productID);
                    Session["products"] = myProducts;
                    Page.Response.Redirect("./Cart.aspx");
                }
                // 2.  They change it and its between 0 and what we have
                
                else if (CheckQuantity(storeQuantity, reqQuantity))
                {
                    manageSessionProducts(productID, reqQuantity);
                    Page.Response.Redirect("./Cart.aspx");

                    // 3. They set it to a quantity that is more than what we have -- the method CheckQuantity will check for this and
                    // update message label with appropriate error
                }
                
            }

            
        }

        protected bool CheckQuantity(int storeQuantity, int requestQuantity)
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


        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }

        protected void btnOrder_Click(object sender, EventArgs e)
        {
            #region Update database to remove quantity selected and add row to order history table

            // Get last order ID and add one to create the new order ID for this order
            string orderQuery = "SELECT MAX(orderID) FROM [OrderHistory]";
            int orderID = myUtils.ExecuteScalarReturn(orderQuery)+1;

            // Get Date for order table
            DateTime date = DateTime.Now;
                        
            // Build Query Strings
            string queryStringProduct = "UPDATE Product SET productQuantity = @prodQuantity WHERE productID = @prodID";
            string queryStringOrder = "INSERT INTO OrderHistory (orderID, productID, date, userID, quantity, price, productName) VALUES (@orderID, @prodID, @date, @userID, @prodQuantity, @prodPrice, @prodName)";

            int result = 0;

            foreach (GridViewRow row in GridView1.Rows)
            {
                #region UPDATE PRODUCT TABLE
                // Make multiple objects so that the SqlParameter naming issue does not occur
                SqlUtil sqlUtils = new SqlUtil();

                // Get quantity
                TextBox txtReqQuantity = (TextBox)row.FindControl("txtOrderQuantity");
                int reqQuantity = Convert.ToInt32(txtReqQuantity.Text);
                int currentQuantity = Convert.ToInt32(row.Cells[3].Text);
                int updatedQuantity = (currentQuantity - reqQuantity);

                // Get prodID
                string prodID = GridView1.DataKeys[row.RowIndex].Values[0].ToString();

                SqlParameter[] updateProductParam = new SqlParameter[] {
                    new SqlParameter("@prodQuantity", updatedQuantity),
                    new SqlParameter("@prodID", prodID)
                };

                result += sqlUtils.ExecuteNonQueryReturn(queryStringProduct, updateProductParam);
                #endregion


                // Get product price and name
                double prodPrice = Convert.ToDouble(row.Cells[2].Text);
                string prodName = row.Cells[0].Text;

                #region UPDATE ORDER TABLE
                SqlParameter[] updateOrderParam = new SqlParameter[] {
                    new SqlParameter("@orderID", orderID),
                    new SqlParameter("@prodID", prodID),
                    new SqlParameter("@date", date),
                    new SqlParameter("@userID", Convert.ToInt32(Session["userID"].ToString())),
                    new SqlParameter("@prodQuantity", reqQuantity),
                    new SqlParameter("@prodPrice", prodPrice),
                    new SqlParameter("@prodName", prodName)
                };
                result += sqlUtils.ExecuteNonQueryReturn(queryStringOrder, updateOrderParam);

                #endregion
            }

            if ((GridView1.Rows.Count*2) == result)
            {
                // op success
            }

            #endregion
            // Redirect to user's order history page in lieu of confirmation page
        }
    }
}