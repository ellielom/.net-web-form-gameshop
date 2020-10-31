using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FinalProject.DataSet1TableAdapters;

namespace FinalProject
{
    /// <summary>
    /// 
    /// Principal Author: Sahijkar Sanghara
    /// 
    /// Purpose: Allow users to register to be allowed to enter the site using their specific credentials.
    /// User information is saved to the database for future logins.
    /// Once registered users are redirected to the login page before having access to the site. 
    /// 
    /// </summary>
    public partial class Registration : System.Web.UI.Page
    {
        UserTableAdapter tblAdapter;
        DataSet1.UserDataTable dataTable;

        User1TableAdapter tba;
        DataSet1.User1DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            tblAdapter = new UserTableAdapter();
            dataTable = new DataSet1.UserDataTable();

            tba = new User1TableAdapter();
            dt = new DataSet1.User1DataTable();

        }
        /// <summary>
        /// 
        /// Register click button saves the full name, username, password and shipping address of the newly signed up user to the database,
        /// Also handles exceptions such as same username and leaving any field blank. Once registered successfully user is redirected back to
        /// the login page. 
        /// </summary>

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtFullName.Text) || !String.IsNullOrWhiteSpace(txtUsername.Text) || !String.IsNullOrWhiteSpace(txtPassword.Text) || !String.IsNullOrWhiteSpace(txtShippingAddress.Text))
            {
                if (tba.GetDataByUser(txtUsername.Text, txtPassword.Text).Rows.Count != 0)
                {
                    lblMsg.Text = "Username already exists, try again.";
                    lblMsg.ForeColor = System.Drawing.Color.Red;

                    txtFullName.Text = "";
                    txtUsername.Text = "";
                    txtPassword.Text = "";
                    txtShippingAddress.Text = "";
                }
                else
                {
                    int res = tblAdapter.Insert(txtFullName.Text, txtUsername.Text, txtPassword.Text, txtShippingAddress.Text);

                    if (res == 1)
                    {
                        Response.Redirect("Login.aspx?lblMsg=User Created Successfully.");
                    }
                }

            }
            else
            {
                lblMsg.Text = "Please fill out all fields.";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }

        }
        /// <summary>
        /// 
        /// Clicking the cancel button will redirect the user to the login page. 
        /// 
        /// </summary>

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}
    
