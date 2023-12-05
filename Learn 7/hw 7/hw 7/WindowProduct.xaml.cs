using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для WindowProduct.xaml
    /// </summary>
    public partial class WindowProduct : Window
    {
        private string conn;
        private Products product;

        public WindowProduct(string conn)
        {
            InitializeComponent();
            this.conn = conn;
            LoadWindow();
        }

        private void LoadWindow()
        {
            string sql = "SELECT * FROM Products";
            using (SqlConnection db = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand(sql, db);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dT = new DataTable();
                adapter.Fill(dT);
                ProductListView.ItemsSource = dT.DefaultView;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            WindowAddProduct windowAddProduct = new WindowAddProduct(conn);
            windowAddProduct.Show();
            LoadWindow();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductListView.SelectedItems != null)
            {
                if (ProductListView.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Выберите товар для удаления");
                    return;
                }
                else
                {
                    DataRowView row = (DataRowView)ProductListView.SelectedItems[0];
                    string sql = "delete from Products where Id = @Id";
                    using (SqlConnection db = new SqlConnection(conn))
                    {
                        SqlCommand cmd = new SqlCommand(sql, db);
                        cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(row["Id"]));
                        db.Open();
                        cmd.ExecuteNonQuery();
                    }
                    LoadWindow();
                }
            }
        }

        private void EditProduct_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ProductListView.SelectedItems != null) // проверяем, что выделена хотя бы одна строка
            {
                DataRowView row = (DataRowView)ProductListView.SelectedItems[0]; // получаем выделенную строку
                Products products = new Products(); // создаем экземпляр класса Products
                products.Id = Convert.ToInt32(row["Id"]); // присваиваем значения свойствам экземпляра класса
                products.Name = row["Name"].ToString();
                products.Model = row["Model"].ToString();
                products.Price = Convert.ToDecimal(row["Price"]);
                products.Count = Convert.ToInt32(row["Count"]);
                EditProduct editProduct = new EditProduct(conn, products);
                editProduct.Show();
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            ProductListView.ItemsSource = null;
            LoadWindow();
        }
    }
}
