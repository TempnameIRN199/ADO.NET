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
    /// Логика взаимодействия для WindowShow.xaml
    /// </summary>
    public partial class WindowShow : Window
    {
        private string conn;

        public WindowShow(string conn)
        {
            InitializeComponent();
            this.conn = conn;
        }

        private void ShowProducts_Click(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT * FROM Products";

            using (SqlConnection db = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand(sql, db);
                db.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dT = new DataTable();
                adapter.Fill(dT);

                AllShowListView.ItemsSource = null;
                AllShowListView.Items.Clear();

                GridView gridView = new GridView();
                AllShowListView.View = gridView;

                gridView.Columns.Clear();

                foreach (DataColumn column in dT.Columns)
                {
                    GridViewColumn gridViewColumn = new GridViewColumn();
                    gridViewColumn.Header = column.ColumnName;

                    Binding binding = new Binding(column.ColumnName);
                    gridViewColumn.DisplayMemberBinding = binding;

                    gridView.Columns.Add(gridViewColumn);
                }

                AllShowListView.ItemsSource = dT.DefaultView;
                db.Close();
            }
        }

        private void ShowSellers_Click(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT * FROM Sellers";

            using (SqlConnection db = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand(sql, db);
                db.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dT = new DataTable();
                adapter.Fill(dT);

                AllShowListView.ItemsSource = null;
                AllShowListView.Items.Clear();

                GridView gridView = new GridView();
                AllShowListView.View = gridView;

                gridView.Columns.Clear();

                foreach (DataColumn column in dT.Columns)
                {
                    GridViewColumn gridViewColumn = new GridViewColumn();
                    gridViewColumn.Header = column.ColumnName;

                    Binding binding = new Binding(column.ColumnName);
                    gridViewColumn.DisplayMemberBinding = binding;

                    gridView.Columns.Add(gridViewColumn);
                }

                AllShowListView.ItemsSource = dT.DefaultView;
                db.Close();
            }
        }

        private void ShowBuyers_Click(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT * FROM Buyers";

            using (SqlConnection db = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand(sql, db);
                db.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dT = new DataTable();
                adapter.Fill(dT);

                AllShowListView.ItemsSource = null;
                AllShowListView.Items.Clear();

                GridView gridView = new GridView();
                AllShowListView.View = gridView;

                gridView.Columns.Clear();

                foreach (DataColumn column in dT.Columns)
                {
                    GridViewColumn gridViewColumn = new GridViewColumn();
                    gridViewColumn.Header = column.ColumnName;

                    Binding binding = new Binding(column.ColumnName);
                    gridViewColumn.DisplayMemberBinding = binding;

                    gridView.Columns.Add(gridViewColumn);
                }

                AllShowListView.ItemsSource = dT.DefaultView;
                db.Close();
            }
        }

        private void ShowSales_Click(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT * FROM Sales";

            using (SqlConnection db = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand(sql, db);
                db.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dT = new DataTable();
                adapter.Fill(dT);

                AllShowListView.ItemsSource = null;
                AllShowListView.Items.Clear();

                GridView gridView = new GridView();
                AllShowListView.View = gridView;

                gridView.Columns.Clear();

                foreach (DataColumn column in dT.Columns)
                {
                    GridViewColumn gridViewColumn = new GridViewColumn();
                    gridViewColumn.Header = column.ColumnName;

                    Binding binding = new Binding(column.ColumnName);
                    gridViewColumn.DisplayMemberBinding = binding;

                    gridView.Columns.Add(gridViewColumn);
                }

                AllShowListView.ItemsSource = dT.DefaultView;
                db.Close();
            }
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            WindowAddProduct windowAddProduct = new WindowAddProduct(conn);
            windowAddProduct.Show();
        }

        private void AddSeller_Click(object sender, RoutedEventArgs e)
        {
            WindowSellers windowSellers = new WindowSellers(conn);
            windowSellers.Show();
        }

        private void AddBuyer_Click(object sender, RoutedEventArgs e)
        {
            WindowBuyer windowBuyers = new WindowBuyer(conn);
            windowBuyers.Show();
        }

        private void AddSale_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
