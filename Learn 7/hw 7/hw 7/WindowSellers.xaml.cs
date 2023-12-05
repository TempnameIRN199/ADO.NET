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

namespace hw_7
{
    /// <summary>
    /// Логика взаимодействия для WindowSellers.xaml
    /// </summary>
    public partial class WindowSellers : Window
    {
        private string conn;

        public WindowSellers(string conn)
        {
            InitializeComponent();
            this.conn = conn;
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            string checkIfExistsQuery = "SELECT COUNT(*) FROM Sellers WHERE FName = @FName";
            string updateSellerQuery = "UPDATE Sellers SET Email = @Email, Phone = @Phone WHERE FName = @FName";
            string addSellerQuery = "INSERT INTO Sellers (FName, Email, Phone) VALUES (@FName, @Email, @Phone)";

            using (SqlConnection db = new SqlConnection(conn))
            {
                SqlCommand command = new SqlCommand(checkIfExistsQuery, db);
                command.Parameters.AddWithValue("@FName", txtName.Text);

                db.Open();
                int existingRowCount = (int)command.ExecuteScalar();

                if (existingRowCount > 0)
                {
                    command = new SqlCommand(updateSellerQuery, db);
                    command.Parameters.AddWithValue("@FName", txtName.Text);
                    command.Parameters.AddWithValue("@Email", txtEmail.Text);
                    command.Parameters.AddWithValue("@Phone", txtPhone.Text);
                    command.ExecuteNonQuery();
                }
                else
                {
                    command = new SqlCommand(addSellerQuery, db);
                    command.Parameters.AddWithValue("@FName", txtName.Text);
                    command.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(txtEmail.Text) ? (object)DBNull.Value : txtEmail.Text);
                    command.Parameters.AddWithValue("@Phone", txtPhone.Text);
                    command.ExecuteNonQuery();
                }
            }

            this.Close();
        }
    }
}
