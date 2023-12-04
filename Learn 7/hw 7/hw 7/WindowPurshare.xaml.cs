using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
    /// Логика взаимодействия для WindowPurshare.xaml
    /// </summary>
    public partial class WindowPurshare : Window
    {
        private string conn;
        private Products products;

        public WindowPurshare(string conn)
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

        private void addToPurshare_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            // это метод для добавления товара в корзину,
            // при добавлении товара в корзину, он удаляется из списка товаров либо уменьшается его количество
            // и добавляється в PurshareListView с количеством 1 шт. и ценой за 1 шт
            // в случае, если товар был добавлен повторно, то увеличивается количество товара в корзине и цена за него

            //string sql = "SELECT * FROM Products WHERE Id = @Id"; // выбираем товар из списка товаров
            //string sql2 = "INSERT INTO Purshare (Id, Name, Price, Quantity) VALUES (@Id, @Name, @Price, @Quantity)"; // добавляем товар в корзину
            //string sql3 = "DELETE FROM Products WHERE Id = @Id"; // удаляем товар из списка товаров
            //string sql4 = "UPDATE Purshare SET Quantity = Quantity + 1 WHERE Id = @Id"; // увеличиваем количество товара в корзине
            //string sql6 = "UPDATE Purshare SET Price = Price * Quantity WHERE Id = @Id"; // умножаем цену за товар в корзине на его количество

            DataRowView rowView = ProductListView.SelectedItem as DataRowView;
            if (rowView != null)
            {
                Products selectedProduct = new Products
                {
                    Id = int.Parse(rowView["Id"].ToString()),
                    Name = rowView["Name"].ToString(),
                    Price = decimal.Parse(rowView["Price"].ToString()),
                    Count = int.Parse(rowView["Count"].ToString())
                };
                // rest of your code
                // Добавление выбранного товара в PurshareListView
                PurshareListView.Items.Add(selectedProduct);

                using (SqlConnection db = new SqlConnection(conn))
                {
                    db.Open();
                    string insertQuery = "INSERT INTO SaleProducts (SaleId, ProductId, Quantity, Price) VALUES (@SaleId, @ProductId, @Quantity, @Price)";

                    using (SqlCommand command = new SqlCommand(insertQuery, db))
                    {
                        // Предположим, что у вас есть значения SaleId, Quantity и Price
                        int saleId = 1; // Пример SaleId

                        command.Parameters.AddWithValue("@SaleId", saleId);
                        command.Parameters.AddWithValue("@ProductId", selectedProduct.Id);
                        command.Parameters.AddWithValue("@Quantity", selectedProduct.Count);
                        command.Parameters.AddWithValue("@Price", selectedProduct.Price); // Пример цены за товар

                        command.ExecuteNonQuery();
                    }

                    db.Close();
                }
            }
        }

        private void returnProduct_DoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}