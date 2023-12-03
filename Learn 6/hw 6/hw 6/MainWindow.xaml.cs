using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace hw_6
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string conn = ConfigurationManager.ConnectionStrings["MyConn"].ConnectionString;
        private List<Users> user;

        public MainWindow()
        {
            InitializeComponent();
            LoadUser();
        }

        private void LoadUser() 
        {
            user = new List<Users>();

            using (SqlConnection db = new SqlConnection(conn))
            {
                string sql = "SELECT * FROM Users";
                SqlCommand cmd = new SqlCommand(sql, db);
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                ad.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    user.Add(new Users
                    {
                        Id = (int)row["Id"],
                        Login = row["Login"].ToString(),
                        PasswordHash = row["PasswordHash"].ToString(),
                        Address = row["Address"].ToString(),
                        Phone = row["Phone"].ToString(),
                        IsAdmin = (bool)row["IsAdmin"]
                    });
                }
                listBoxUsers.ItemsSource = user;
            }
        }

        private void ListBoxUsers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listBoxUsers.SelectedItems != null)
            {
                Users selectedUser = (Users)listBoxUsers.SelectedItem;
                EditUser editUser = new EditUser(conn, selectedUser);
                editUser.ShowDialog();
                LoadUser();
            }
        }

        private void ButtonAddUser_Click(object sender, RoutedEventArgs e)
        {
            AddUser addUser = new AddUser(conn);
            addUser.ShowDialog();
            LoadUser();
        }

        private void ButtonDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            DeleteUser deleteUser = new DeleteUser(conn);
            deleteUser.ShowDialog();
            LoadUser();
        }
        
        private void CheckBoxAdmin_Checked(object sender, RoutedEventArgs e)
        {
            listBoxUsers.ItemsSource = listBoxUsers.Items.Cast<Users>().Where(u => u.IsAdmin).ToList();
        }

        private void CheckBoxAdmin_Unchecked(object sender, RoutedEventArgs e)
        {
            listBoxUsers.ItemsSource = user;
        }
    }
}
