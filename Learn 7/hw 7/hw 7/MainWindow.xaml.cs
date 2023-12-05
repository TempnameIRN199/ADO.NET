using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace hw_7
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string conn = ConfigurationManager.ConnectionStrings["kuma"].ConnectionString;
        private List<Products> products;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowCart_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowProduct_Click(object sender, RoutedEventArgs e)
        {
            WindowProduct productWindow = new WindowProduct(conn);
            productWindow.Show();
        }

        private void ShowPurchaseWindow_Click(object sender, RoutedEventArgs e)
        {
            WindowPurshare windowPurshare = new WindowPurshare(conn);
            windowPurshare.Show();
        }

        private void AddSeller_Click(object sender, RoutedEventArgs e)
        {
            WindowSellers windowSellers = new WindowSellers(conn);
            windowSellers.Show();
        }

        private void AddBuyer_Click(object sender, RoutedEventArgs e)
        {
            WindowBuyer windowBuyer = new WindowBuyer(conn);
            windowBuyer.Show();
        }

        private void ShowTable_Click(object sender, RoutedEventArgs e)
        {
            WindowShow windowShow = new WindowShow(conn);
            windowShow.Show();
        }
    }
}
