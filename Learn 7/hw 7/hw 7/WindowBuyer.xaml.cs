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
    /// Логика взаимодействия для WindowBuyer.xaml
    /// </summary>
    public partial class WindowBuyer : Window
    {
        private string conn;

        public WindowBuyer(string conn)
        {
            InitializeComponent();
            this.conn = conn;
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            string checkIfExistsQuery = "SELECT COUNT(*) FROM Buyers WHERE FName = @FName";
            string updateBuyerQuery = "UPDATE Buyers SET Email = @Email, Phone = @Phone WHERE FName = @FName";
            string addBuyerQuery = "INSERT INTO Buyers (FName, Email, Phone) VALUES (@FName, @Email, @Phone)";

            using (SqlConnection connection = new SqlConnection(conn))
            {
                SqlCommand command = new SqlCommand(checkIfExistsQuery, connection);
                command.Parameters.AddWithValue("@FName", txtName.Text);

                connection.Open();
                int existingRowCount = (int)command.ExecuteScalar();

                if (existingRowCount > 0)
                {
                    command = new SqlCommand(updateBuyerQuery, connection);
                    command.Parameters.AddWithValue("@FName", txtName.Text);
                    command.Parameters.AddWithValue("@Email", txtEmail.Text);
                    command.Parameters.AddWithValue("@Phone", txtPhone.Text);
                    command.ExecuteNonQuery();
                }
                else
                {
                    command = new SqlCommand(addBuyerQuery, connection);
                    command.Parameters.AddWithValue("@FName", txtName.Text);
                    command.Parameters.AddWithValue("@Email", txtEmail.Text);
                    command.Parameters.AddWithValue("@Phone", txtPhone.Text);
                    command.ExecuteNonQuery();
                }
            }

            this.Close();
        }
    }
}
