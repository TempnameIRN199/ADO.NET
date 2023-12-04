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
    /// Логика взаимодействия для EditProduct.xaml
    /// </summary>
    public partial class EditProduct : Window
    {
        private string conn;
        private Products products;

        public EditProduct(string conn, Products products)
        {
            InitializeComponent();
            this.conn = conn;
            this.products = products;
            LoadDataProduct();
        }

        private void LoadDataProduct()
        {
            txtName.Text = products.Name;
            txtModel.Text = products.Model;
            txtPrice.Text = products.Price.ToString();
            txtCount.Text = products.Count.ToString();
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            WindowProduct windowProduct = new WindowProduct(conn);
            string sql = "update Products set Name = @Name, Model = @Model, Price = @Price, Count = @Count where Id = @Id";

            using (SqlConnection db = new SqlConnection(conn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(sql, db);
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Model", txtModel.Text);
                    cmd.Parameters.AddWithValue("@Price", Convert.ToDecimal(txtPrice.Text));
                    cmd.Parameters.AddWithValue("@Count", Convert.ToInt32(txtCount.Text));
                    cmd.Parameters.AddWithValue("@Id", products.Id);
                    db.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
                finally
                {
                    MessageBox.Show("Данные успешно изменены");
                    db.Close();
                    this.Close();
                }
            }
        }
    }
}
