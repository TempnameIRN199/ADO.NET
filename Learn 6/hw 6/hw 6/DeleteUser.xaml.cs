using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Configuration;
using System.Windows.Navigation;

namespace hw_6
{
    /// <summary>
    /// Логика взаимодействия для DeleteUser.xaml
    /// </summary>
    public partial class DeleteUser : Window
    {
        private string conn;
        private List<Users> users = new List<Users>();

        public DeleteUser(string conn)
        {
            InitializeComponent();
            this.conn = conn;
            LoadDeleteUser();
        }

        private void LoadDeleteUser()
        {
            using (SqlConnection db = new SqlConnection(conn))
            {
                string sql = "SELECT * FROM Users";
                SqlCommand cmd = new SqlCommand(sql, db);
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                ad.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    users.Add(new Users
                    {
                        Id = (int)row["Id"],
                        Login = row["Login"].ToString(),
                        PasswordHash = row["PasswordHash"].ToString(),
                        Address = row["Address"].ToString(),
                        Phone = row["Phone"].ToString(),
                        IsAdmin = (bool)row["IsAdmin"]
                    });
                }
                listBoxUsers.ItemsSource = users;
            }
        }

        private void ListBoxUsers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listBoxUsers.SelectedItems != null)
            {
                Users user = (Users)listBoxUsers.SelectedItem;

                using (SqlConnection db = new SqlConnection(conn))
                {
                    string sql = "delete from Users where Id = @UserId";
                    SqlCommand cmd = new SqlCommand(sql, db);
                    cmd.Parameters.AddWithValue("@UserId", user.Id);

                    try
                    {
                        db.Open();
                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Пользователь успешно удален");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Не удалось удалить пользователя");
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
        }
    }
}
