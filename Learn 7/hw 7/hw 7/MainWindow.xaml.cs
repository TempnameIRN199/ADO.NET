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

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowCart_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowProduct_Click(object sender, RoutedEventArgs e)
        {
            WindowProduct productWindow = new WindowProduct();
            productWindow.Show();
        }

        private void ShowPurchaseWindow_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
