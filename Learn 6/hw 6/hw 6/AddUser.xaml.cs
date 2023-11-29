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
        private kuma kum;

        public AddUser(string conn)
        {
            InitializeComponent();
            this.conn = conn;
        }

        private string GetPasswordHash(string password)
        {
            return password.GetHashCode().ToString();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            string login = txtLogin.Text;

            if (!string.IsNullOrWhiteSpace(login))
            {
                bool isUnique = kum.IsLoginUnique(login, 0, conn);

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
