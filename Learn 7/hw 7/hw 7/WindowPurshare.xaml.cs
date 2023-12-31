﻿using System;
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
using Newtonsoft.Json;
using System.IO;

namespace hw_7
{
    /// <summary>
    /// Логика взаимодействия для WindowPurshare.xaml
    /// </summary>
    public partial class WindowPurshare : Window
    {
        private string conn;

        private int selectedBuyerId = 0;
        private int selectedSellerId = 0;
        private int fileCount = 1;

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
            WindowChoose chooseSellerBuyerWindow = new WindowChoose(conn);

            if (chooseSellerBuyerWindow.ShowDialog() == true)
            {
                
            }
            else
            {
                int selectedBuyerId = chooseSellerBuyerWindow.SelectedBuyerId;
                int selectedSellerId = chooseSellerBuyerWindow.SelectedSellerId;

                MessageBox.Show($"BuyerId: {selectedBuyerId}\nSellerId: {selectedSellerId}");

                string sql = "SELECT TOP 1 * FROM Sales ORDER BY Id DESC";

                using (SqlConnection db = new SqlConnection(conn))
                {
                    SqlCommand cmd = new SqlCommand(sql, db);
                    db.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dT = new DataTable();
                    adapter.Fill(dT);

                    PurshareListView.ItemsSource = null;
                    PurshareListView.Items.Clear();

                    GridView gridView = new GridView();
                    PurshareListView.View = gridView;

                    gridView.Columns.Clear();

                    foreach (DataColumn column in dT.Columns)
                    {
                        GridViewColumn gridViewColumn = new GridViewColumn();
                        gridViewColumn.Header = column.ColumnName;

                        Binding binding = new Binding(column.ColumnName);
                        gridViewColumn.DisplayMemberBinding = binding;

                        gridView.Columns.Add(gridViewColumn);
                    }

                    PurshareListView.ItemsSource = dT.DefaultView;

                    PurshareListView.ItemsSource = null;
                    PurshareListView.Items.Clear();

                    dT.Clear();
                    adapter.Dispose();
                    cmd.Dispose();


                    if (ProductListView.SelectedItems != null)
                    {
                        DataRowView row = (DataRowView)ProductListView.SelectedItems[0];
                        sql = "SELECT TOP 1 * FROM Sales ORDER BY Id DESC";
                        cmd = new SqlCommand(sql, db);
                        adapter = new SqlDataAdapter(cmd);
                        dT = new DataTable();
                        adapter.Fill(dT);
                        int saleId = Convert.ToInt32(dT.Rows[0]["Id"]);
                        sql = "INSERT INTO SaleProducts (SaleId, ProductId, Quantity, Price) VALUES (@SaleId, @ProductId, @Quantity, @Price)";
                        cmd = new SqlCommand(sql, db);
                        cmd.Parameters.AddWithValue("@SaleId", saleId);
                        cmd.Parameters.AddWithValue("@ProductId", Convert.ToInt32(row["Id"]));
                        cmd.Parameters.AddWithValue("@Quantity", 1);
                        cmd.Parameters.AddWithValue("@Price", Convert.ToDecimal(row["Price"]));
                        cmd.ExecuteNonQuery();

                        sql = "SELECT TOP 1 * FROM SaleProducts ORDER BY Id DESC";
                        cmd = new SqlCommand(sql, db);
                        adapter = new SqlDataAdapter(cmd);
                        dT = new DataTable();
                        adapter.Fill(dT);

                        PurshareListView.ItemsSource = null;
                        PurshareListView.Items.Clear();

                        gridView = new GridView();
                        PurshareListView.View = gridView;
                        gridView.Columns.Clear();

                        foreach (DataColumn column in dT.Columns)
                        {
                            GridViewColumn gridViewColumn = new GridViewColumn();
                            gridViewColumn.Header = column.ColumnName;

                            Binding binding = new Binding(column.ColumnName);
                            gridViewColumn.DisplayMemberBinding = binding;

                            gridView.Columns.Add(gridViewColumn);
                        }

                        PurshareListView.ItemsSource = dT.DefaultView;
                    }

                    db.Close();
                }
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            LoadWindow();

            string sql = "SELECT TOP 1 * FROM Sales ORDER BY Id DESC";

            using (SqlConnection db = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand(sql, db);
                db.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dT = new DataTable();
                adapter.Fill(dT);

                PurshareListView.ItemsSource = null;
                PurshareListView.Items.Clear();

                GridView gridView = new GridView();
                PurshareListView.View = gridView;

                gridView.Columns.Clear();

                foreach (DataColumn column in dT.Columns)
                {
                    GridViewColumn gridViewColumn = new GridViewColumn();
                    gridViewColumn.Header = column.ColumnName;

                    Binding binding = new Binding(column.ColumnName);
                    gridViewColumn.DisplayMemberBinding = binding;

                    gridView.Columns.Add(gridViewColumn);
                }

                PurshareListView.ItemsSource = dT.DefaultView;
                db.Close();
            }
        }

        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            if (PurshareListView.Items.Count == 0)
            {
                MessageBox.Show("Нечего покупать");
                return;
            }
            else
            {
                string sql = "SELECT TOP 1 * FROM SaleProducts ORDER BY Id DESC";

                using (SqlConnection db = new SqlConnection(conn))
                {
                    SqlCommand cmd = new SqlCommand(sql, db);
                    db.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dT = new DataTable();
                    adapter.Fill(dT);

                    foreach (DataRow row in dT.Rows)
                    {
                        string filename = $"final-{fileCount}.json";
                        while (File.Exists(filename))
                        {
                            fileCount++;
                            filename = $"final-{fileCount}.json";
                        }
                        string json = JsonConvert.SerializeObject(row, Formatting.Indented);

                        File.WriteAllText(filename, json);

                        fileCount++;
                    }
                    PurshareListView.ItemsSource = null;
                    PurshareListView.Items.Clear();


                    db.Close();
                }
            }
        }
    }
}