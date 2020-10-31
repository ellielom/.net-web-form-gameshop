using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FinalProject.DataSet1TableAdapters;

namespace FinalProject
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        CompanyTableAdapter companyTableAdapter;
        DataSet1.CompanyDataTable companyTable;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["isAdmin"] == null || Session["isAdmin"].ToString().Equals("false"))
            {
                Page.Response.Redirect("../Store.aspx");
            }

            companyTableAdapter = new CompanyTableAdapter();
            companyTable = new DataSet1.CompanyDataTable();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtCompany.Text))
            {
                int res = companyTableAdapter.Insert(txtCompany.Text, txtLocation.Text);

                if (res == 1)
                    Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
        }
    }
}