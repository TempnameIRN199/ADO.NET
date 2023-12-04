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
        private List<Buyers> buyers;
        private List<Sellers> sellers;
        private List<Sales> sales;
        private List<SaleProducts> saleProducts;

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

        }

        private void returnProduct_DoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}