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
    /// Логика взаимодействия для WindowAddProduct.xaml
    /// </summary>
    public partial class WindowAddProduct : Window
    {
        private string conn;

        public WindowAddProduct(string conn)
        {
            InitializeComponent();
            this.conn = conn;
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            string updateProd = "update Products set Count = Count + @Count where Name = @Name and Model = @Model";
            string addProd = "insert into Products (Name, Model, Price, Count) values (@Name, @Model, @Price, @Count)";
            string sql = "select * from Products where Name = @Name and Model = @Model and Price = @Price";

            using (SqlConnection db = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand(sql, db);
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@Model", txtModel.Text);
                cmd.Parameters.AddWithValue("@Price", txtPrice.Text);
                cmd.Parameters.AddWithValue("@Count", txtCount.Text);
                db.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Close();
                    cmd = new SqlCommand(updateProd, db);
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Model", txtModel.Text);
                    cmd.Parameters.AddWithValue("@Count", txtCount.Text);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    reader.Close();
                    cmd = new SqlCommand(addProd, db);
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Model", txtModel.Text);
                    cmd.Parameters.AddWithValue("@Price", txtPrice.Text);
                    cmd.Parameters.AddWithValue("@Count", txtCount.Text);
                    cmd.ExecuteNonQuery();
                }
                db.Close();
                this.Close();
            }
        }
    }
}
