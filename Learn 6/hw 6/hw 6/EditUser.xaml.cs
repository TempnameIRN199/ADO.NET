using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Логика взаимодействия для EditUser.xaml
    /// </summary>
    public partial class EditUser : Window
    {
        private string conn;
        private Users user;

        public EditUser(string conn, Users user)
        {
            InitializeComponent();
            this.conn = conn;
            this.user = user;
            LoadUserData();
        }

        private void LoadUserData()
        {
            txtLogin.Text = user.Login;
            txtPassword.Text = user.PasswordHash;
            txtAddress.Text = user.Address;
            txtPhone.Text = user.Phone;
            chkIsAdmin.IsChecked = user.IsAdmin;
        }

        private string GetPasswordHash(string password)
        {
            return password.GetHashCode().ToString();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            user.Login = txtLogin.Text;
            user.PasswordHash = txtPassword.Text;
            user.Address = txtAddress.Text;
            user.Phone = txtPhone.Text;
            user.IsAdmin = (bool)chkIsAdmin.IsChecked;

            user.PasswordHash = GetPasswordHash(user.PasswordHash);

            using (SqlConnection db = new SqlConnection(conn))
            {
                string sql = "select count(*) from Users where Login = @Login and Id != @UserId";
                SqlCommand cmd = new SqlCommand(sql, db);
                cmd.Parameters.AddWithValue("@Login", user.Login);
                cmd.Parameters.AddWithValue("@UserId", user.Id);

                try
                {
                    db.Open();
                    int result = (int)cmd.ExecuteScalar();
                    if (result > 0)
                    {
                        MessageBox.Show("Пользователь с таким логином уже существует");
                        return;
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }

            using (SqlConnection db = new SqlConnection(conn))
            {
                string sql = "update Users set Login = @Login, Address = @Address, Phone = @Phone, IsAdmin = @IsAdmin WHERE Id = @UserId";

                SqlCommand cmd = new SqlCommand(sql, db);
                cmd.Parameters.AddWithValue("@Login", user.Login);
                cmd.Parameters.AddWithValue("@Address", user.Address);
                cmd.Parameters.AddWithValue("@Phone", user.Phone);
                cmd.Parameters.AddWithValue("@IsAdmin", user.IsAdmin);
                cmd.Parameters.AddWithValue("@UserId", user.Id);

                try
                {
                    db.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0) {
                        MessageBox.Show("Данные успешно обновлены");
                        this.Close();
                    } else
                        MessageBox.Show("Ошибка обновления данных");
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}


// проверка 
// select count(*) from Users where Login = @Login and Id != @UserId"