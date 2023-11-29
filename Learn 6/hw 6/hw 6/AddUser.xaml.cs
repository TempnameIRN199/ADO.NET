using System;
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

namespace hw_6
{
    /// <summary>
    /// Логика взаимодействия для AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {

        private string conn;
        private Users user;

        public AddUser(string conn)
        {
            InitializeComponent();
            this.conn = conn;
        }

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

        public string GetPasswordHash(string password)
        {
            return password.GetHashCode().ToString();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            string login = txtLogin.Text;

            if (!string.IsNullOrWhiteSpace(login))
            {
                user = new Users();
                bool isUnique = IsLoginUnique(login, user.Id, conn);

                if (!isUnique)
                {
                    MessageBox.Show("Такой логин уже существует");
                    return;
                }

                using (SqlConnection db = new SqlConnection(conn))
                {
                    string sql = "insert into Users (Login, PasswordHash, Address, Phone, IsAdmin) values (@Login, @PasswordHash, @Address, @Phone, @IsAdmin)";
                    SqlCommand cmd = new SqlCommand(sql, db);
                    cmd.Parameters.AddWithValue("@Login", login);
                    cmd.Parameters.AddWithValue("@PasswordHash", GetPasswordHash(txtPassword.Text));
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                    cmd.Parameters.AddWithValue("@IsAdmin", chkIsAdmin.IsChecked);

                    try
                    {
                        db.Open();
                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Пользователь успешно добавлен");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Не удалось добавить пользователя");
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message); 
                        throw;
                    }
                }
            }
            else
            {
                MessageBox.Show("Введите логин");
            }
        }
    }
}
