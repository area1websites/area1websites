using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace Dist23MVC.Models
{
    class clsDataGetter
    {
        public SqlConnection conn;
        string cnStr;

        public clsDataGetter(string connStr)
        {
            conn = new System.Data.SqlClient.SqlConnection(connStr);
            cnStr = connStr;
        }

        public System.Data.SqlClient.SqlDataReader GetDataReader(string sql,SqlConnection newConn=null)
        {
            System.Data.SqlClient.SqlDataReader dr = null;
            if (newConn == null)
            {
                newConn = conn;
            }

            if (newConn.State != ConnectionState.Open)
            {
                newConn.Open();
            }

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sql, newConn);
            cmd.CommandTimeout = 3600;
            try
            {
                dr = cmd.ExecuteReader();
            }
            catch
            {
                newConn.Close();
            }
            return dr;
        }

        public void KillReader(SqlDataReader dr) 
        {
            dr.Close();
            dr.Dispose();
        }
        public DataSet GetDataSet(string sql)
        {
            System.Data.SqlClient.SqlConnection conn3 = new System.Data.SqlClient.SqlConnection(cnStr);
            System.Data.SqlClient.SqlDataAdapter adapt = new System.Data.SqlClient.SqlDataAdapter(sql, conn3);
            DataSet ds = new DataSet();
            adapt.Fill(ds);
            conn3.Close();
            conn3.Dispose();
            conn3 = null;
            return ds;
        }

        public int GetScalarInteger(string sql)
        {
            int x = -1;
            System.Data.SqlClient.SqlConnection conn3 = new System.Data.SqlClient.SqlConnection(cnStr);
            conn3.Open();
            SqlCommand cmd = new SqlCommand(sql, conn3);
            x = (int)cmd.ExecuteScalar();
            conn3.Close();
            conn3.Dispose();
            conn3 = null;
            return x;
        }

        public bool GetScalarBoolean(string sql)
        {
            bool x;
            System.Data.SqlClient.SqlConnection conn3 = new System.Data.SqlClient.SqlConnection(cnStr);
            conn3.Open();
            SqlCommand cmd = new SqlCommand(sql, conn3);
            x = (bool)cmd.ExecuteScalar();
            conn3.Close();
            conn3.Dispose();
            conn3 = null;
            return x;
        }

        public string GetScalarString(string sql)
        {
            string x = "";
            System.Data.SqlClient.SqlConnection conn3 = new System.Data.SqlClient.SqlConnection(cnStr);
            conn3.Open();
            SqlCommand cmd = new SqlCommand(sql, conn3);
            object result = cmd.ExecuteScalar();
            if (result.ToString() == "")
            {
                x = "";
            }
            else
            {
                x = (string)result;
            }
            conn3.Close();
            conn3.Dispose();
            conn3 = null;
            return x;
        }

        public bool HasData(string sql,SqlConnection newConn=null)
        {
            System.Data.SqlClient.SqlDataReader dr;
            if (newConn == null)
            {
                newConn = conn;
            }

            if (newConn.State != System.Data.ConnectionState.Open)
            {
                newConn.Open();
            }

            SqlCommand cmd = new SqlCommand(sql, newConn);
            cmd.CommandTimeout = 3600;
            try
            {
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    dr.Close();
                    newConn.Close();
                    return true;
                }
                else
                {
                    dr.Close();
                    newConn.Close();
                    return false;
                }
            }
            catch
            {
                newConn.Close();
                return false;
            }
        }

        public string RunCommand(string sql)
        {
            if (conn.State != System.Data.ConnectionState.Open)
            {
                conn.Open();
            }

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandTimeout = 6000000;
            try
            {
                cmd.ExecuteNonQuery();
                return "";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

    }
}



