using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace hw_6
{
    public class kuma
    {
        public bool IsLoginUnique(string loign, int Id, string conn)
        {
            using (SqlConnection db = new SqlConnection(conn))
            {
                string sql = "select count(*) from Users where Login = @Login and Id != @UserId";
                SqlCommand cmd = new SqlCommand(sql, db);
                cmd.Parameters.AddWithValue("@Login", loign);
                cmd.Parameters.AddWithValue("@UserId", Id);

                try
                {
                    db.Open();
                    int result = (int)cmd.ExecuteScalar();
                    return result == 0;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
            }
        }
    }
}
