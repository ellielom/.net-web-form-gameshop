using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FinalProject.DataSet1TableAdapters;
using FinalProject;
using System.Data.SqlClient;

namespace FinalProject
{
    public partial class Login : System.Web.UI.Page
    {
        /// <summary>
        /// 
        /// Principal Author: Ebrahim Zhalehsani
        /// 
        /// Purpose: Allow user/admin to login with credentials or go to the registration page for sign up
        /// the textboxes are validated and user can not leave them empty.
        /// gives error if the user does not exists
        /// 
        /// </summary>
        User1TableAdapter tba;
        DataSet1.User1DataTable udt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["lblMsg"] != null)
            {
                lblMsg.Text = Request.QueryString["lblMsg"];
            }
            tba = new User1TableAdapter();
            udt = new DataSet1.User1DataTable();
        }
        ///<summary>
        ///
        /// Allows user/admin to login with credentials or go to the registration page for sign up
        /// the textboxes are validated and user can not leave them empty.
        /// gives error if the user does not exists 
        /// 
        /// </summary>

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string username = txt1.Text;
            string password = txt2.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrEmpty(username))
            {
                lblMsg.Text = "Please fill the User Name field";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            else if (string.IsNullOrWhiteSpace(password) || string.IsNullOrEmpty(password))
            {
                lblMsg.Text = "Please fill the Password field";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            else if (tba.GetDataByUser(username, password).Rows.Count == 0)
            {
                lblMsg.Text = "User Not Found";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                //        public int ExecuteScalarReturn(string queryString)
                SqlUtil myUtilts = new SqlUtil();


                lblMsg.Text = "";
                var row = tba.GetDataByUser(username, password);

                string queryString = "SELECT isAdmin FROM [User] WHERE username = @username";
                SqlParameter sqlParam = new SqlParameter("@username", username);

                string isAdmin = myUtilts.ExecuteScalarReturn(queryString, sqlParam);
               
                //string isAdmin = row.Columns["isAdmin"].ToString().ToLower();


                if (isAdmin.Equals("False"))
                {
                    //User Home Page
                    Session["username"] = username;
                    Session["userID"] = row.Rows[0]["userID"];
                    Session["isAdmin"] = "false";
                    Page.Response.Redirect("./Store.aspx");
                    

                }
                else
                {
                    //Admin Home page
                    Session["username"] = username;
                    Session["userID"] = row.Rows[0]["userID"];
                    Session["isAdmin"] = "true";
                    Page.Response.Redirect("./Store.aspx");


                }
            }
        }
        /// <summary>
        /// 
        /// Register button click event redirects user to the register page
        /// 
        /// </summary>

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("Registration.aspx");
        }
    }
}