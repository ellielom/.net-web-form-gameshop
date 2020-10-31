/*Ebi and Ivy*/
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
    public partial class WebForm3 : System.Web.UI.Page
    {
        SqlUtil myUtils = new SqlUtil();
        protected void Page_Load(object sender, EventArgs e)
        {
          
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);

            string orderID = GridView1.Rows[index].Cells[0].Text.ToString();

            string querystring = "select productName, quantity, price from orderHistory where orderid = @orderid";

            SqlParameter[] sqlParam = new SqlParameter[] { new SqlParameter("@orderid", Convert.ToInt32(orderID)) };

            BindGridView(querystring, sqlParam);

            lblIvy.Text = "Receipt Order # " + orderID;

            PrepareProductTotal();
        }

        protected void BindGridView(string sqlQuery, SqlParameter[] param)
        {
            DataTable dt = myUtils.GetDataTables(sqlQuery, param);
            GridView2.DataSource = dt;
            GridView2.DataBind();
        }

        protected void PrepareProductTotal()
        {
            double subtotal = 0;

            foreach (GridViewRow row in GridView2.Rows)
            {
                double price = Convert.ToDouble(row.Cells[2].Text);
                int quantity = Convert.ToInt32(row.Cells[1].Text.ToString());
                subtotal += price * quantity;
            }

            double taxes = subtotal * 0.13;
            double total = subtotal + taxes;

            lblSubtotal.Text = subtotal.ToString("C");
            lblTax.Text = taxes.ToString("C");
            lblTotal.Text = total.ToString("C");


        }
    }
}