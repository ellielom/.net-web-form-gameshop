using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userID"] == null) { 
                login.InnerHtml = "<a href='http://localhost:64700/Login.aspx'>Login</a>";
                
            }
            else
            {
                login.InnerHtml = "<a href='http://localhost:64700/Logout.aspx'>Logout</a>";
                orderHistory.InnerHtml = "<a href='http://localhost:64700/OrderHistory.aspx'>Order History</a>";
            }

            // If user is not an admin, do not show the admin navigation bar 
            if (Session["isAdmin"] == null || Session["isAdmin"].ToString().ToLower().Equals("false"))
            {
                adminNav.Visible = false;
                userNav.Visible = true;
            }
            // User is admin, hide user navigation bar and show admin navigation bar
            else
            {
                adminNav.Visible = true;
                userNav.Visible = false;
            }
        }
    }
}