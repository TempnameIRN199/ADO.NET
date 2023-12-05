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
    /// Логика взаимодействия для WindowChoose.xaml
    /// </summary>
    public partial class WindowChoose : Window
    {
        public int SelectedBuyerId { get; private set; }
        public int SelectedSellerId { get; private set; }
        private string conn;

        public WindowChoose(string conn)
        {
            InitializeComponent();
            this.conn = conn;
            LoadBuyersAndSellers();
        }

        private void LoadBuyersAndSellers()
        {
            using (SqlConnection db = new SqlConnection(conn))
            {
                string sql = "SELECT * FROM Buyers";
                SqlCommand cmd = new SqlCommand(sql, db);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dT = new DataTable();
                adapter.Fill(dT);
                BuyerListView.ItemsSource = dT.DefaultView;

                sql = "SELECT * FROM Sellers";
                cmd = new SqlCommand(sql, db);
                adapter = new SqlDataAdapter(cmd);
                dT = new DataTable();
                adapter.Fill(dT);
                SellerListView.ItemsSource = dT.DefaultView;
            }

        }

        private void BuyerListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRowView selectedBuyerDataRow = (DataRowView)BuyerListView.SelectedItem;
            if (selectedBuyerDataRow != null)
            {
                DataRow selectedBuyer = selectedBuyerDataRow.Row;
                if (selectedBuyer != null && !string.IsNullOrEmpty(selectedBuyer["Id"].ToString()))
                {
                    SelectedBuyerId = Convert.ToInt32(selectedBuyer["Id"]);
                    TryCreateSalesRecord();
                }
            }
        }

        private void SellerListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRowView selectedSellerDataRow = (DataRowView)SellerListView.SelectedItem;
            if (selectedSellerDataRow != null)
            {
                DataRow selectedSeller = selectedSellerDataRow.Row;
                if (selectedSeller != null && !string.IsNullOrEmpty(selectedSeller["Id"].ToString()))
                {
                    SelectedSellerId = Convert.ToInt32(selectedSeller["Id"]);
                    TryCreateSalesRecord();
                }
            }
        }

        private void TryCreateSalesRecord()
        {
            if (SelectedBuyerId != 0 && SelectedSellerId != 0)
            {
                CreateSalesRecord();
                Close();
            }
        }

        private void CreateSalesRecord()
        {
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();

                string insertSalesQuery = "INSERT INTO Sales (BuyerId, SellerId, SaleDate) VALUES (@BuyerId, @SellerId, GETDATE())";
                SqlCommand command = new SqlCommand(insertSalesQuery, connection);
                command.Parameters.AddWithValue("@BuyerId", SelectedBuyerId);
                command.Parameters.AddWithValue("@SellerId", SelectedSellerId);

                command.ExecuteNonQuery();
            }
        }
    }
}
