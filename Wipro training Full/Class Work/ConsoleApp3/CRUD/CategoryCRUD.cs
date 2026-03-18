using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace ConsoleApp3.CRUD
{
    internal class CategoryCRUD

    {
        SqlConnection con = new SqlConnection( ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);

        public void AddCategory(Category c)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into Category values ('" + c.Name + "')", con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public string UpdateCategory(Category c)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update Category set [name]='" + c.Name + "' where id='" + c.Id + "'", con);
                cmd.ExecuteNonQuery();
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;

            }
            finally
            {
                con.Close();
            }
        }
    }
}

