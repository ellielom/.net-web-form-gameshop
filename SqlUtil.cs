using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace FinalProject
{
    /// <summary>
    /// Principal Author: Ellie Lombardi
    /// Purpose: Generic SQL methods that can be reused across several pages/classes.
    /// </summary>
    /// 


    public class SqlUtil
    {
        /// init Connection String for project
        string connString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;



        /// <summary>
        /// Based on parameter given, method will pull results from database
        /// and store it into a DataTable
        /// </summary>
        /// <param name="queryString">string</param>
        /// <returns> DataTable </returns>
        public DataTable GetDataTables(string queryString)
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

        /// <summary>
        /// OVERLOADED METHOD
        /// Based on parameter given, method will pull results from database
        /// and store it into a DataTable
        /// </summary>
        /// <param name="queryString">string</param>
        /// <param name="param">SqlParameter[]</param>
        /// <returns> DataTable </returns>
        public DataTable GetDataTables(string queryString, SqlParameter[] param)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.Parameters.AddRange(param);

                DataSet ds = new DataSet();
                (new SqlDataAdapter(command)).Fill(ds);

                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
                command.Parameters.Clear();
            }
            return dt;
        }

        /// <summary>
        /// OVERLOADED
        /// Based on parameter given, method will pull results from database
        /// and store it into a DataTable
        /// </summary>
        /// <param name="queryString">string</param>
        /// <returns> DataTable </returns>
        public DataTable GetDataTables(string queryString, Dictionary<int, int> dictionary)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand();

                StringBuilder sb = new StringBuilder();
                int i = 1;

                SqlParameter[] sqlProductParams = new SqlParameter[] { };
                foreach (int productID in dictionary.Keys)
                {   
                    // IN clause
                    sb.Append("@productID" + i.ToString() + ",");

                    // parameter
                    command.Parameters.AddWithValue("@productID" + i.ToString(), productID);

                    i++;
                }
                
                //sb.Append(")");
                queryString += sb.ToString();
                queryString = queryString.Substring(0, queryString.Length - 1) + ")";


                command.Connection = connection;
                command.CommandText = queryString;



                connection.Open();

                DataSet ds = new DataSet();
                (new SqlDataAdapter(command)).Fill(ds);

                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }

                command.Parameters.Clear();
            }
            return dt;
        }


        /// <summary>
        /// Generic method that will take in a SQL command such as update, insert, etc
        /// and reutrn a success code
        /// </summary>
        /// <param name="queryString">SQL query string</param>
        /// <param name="prms"> SqlParameter[]</param>
        /// <returns>int</returns>
        public int ExecuteNonQueryReturn(string queryString, SqlParameter[] prms)
        {
            
            int res = 0;
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.Parameters.AddRange(prms);

                res = Convert.ToInt32(command.ExecuteNonQuery());
                command.Parameters.Clear();
            }
            return res;
        }

        // SQL Statement for when there is only one cell returned
        public int ExecuteScalarReturn(string queryString)
        {
            int res = 0;
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                res = Convert.ToInt32(command.ExecuteScalar());
                command.Parameters.Clear();
            }
            return res;
        }

        // SQL Statement for when there is only one cell returned
        public string ExecuteScalarReturn(string queryString, SqlParameter param)
        {
            string res = "";
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.Parameters.Add(param);
                res = command.ExecuteScalar().ToString();
                command.Parameters.Clear();
            }
            return res;
        }

    }
}