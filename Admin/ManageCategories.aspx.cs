using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FinalProject.DataSet1TableAdapters;

namespace FinalProject
{
    public partial class WebForm5 : System.Web.UI.Page
    {

        CategoryTableAdapter categoryTableAdapter;
        DataSet1.CategoryDataTable categoryTable;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["isAdmin"] == null || Session["isAdmin"].ToString().Equals("false"))
            {
                Page.Response.Redirect("../Store.aspx");
            }

            categoryTableAdapter = new CategoryTableAdapter();
            categoryTable = new DataSet1.CategoryDataTable();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtCategory.Text))
            {
                int res = categoryTableAdapter.Insert(txtCategory.Text);

                if (res == 1)
                    Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
        }


    }
}