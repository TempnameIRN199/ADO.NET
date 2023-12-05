using System;
using System.Collections.Generic;
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
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.Win32;
using System.IO;

namespace lab_7
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
            LoadImage();
        }

        private void LoadImage()
        {
            try
            {
                ImageComboBox.Items.Clear();

                using (SqlConnection db = new SqlConnection(conn))
                {
                    string sql = "select Id, [Name] FROM Image";
                    SqlCommand sqlCommand = new SqlCommand(sql, db);
                    db.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        ImageComboBox.Items.Add(new ImageInfo { ImageID = id, ImageName = name });
                    }
                    reader.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFileName = openFileDialog.FileName;
                ImagePath.Text = selectedFileName;

                PreviewImage.Source = new BitmapImage(new Uri(selectedFileName));
            }
        }

        private void SaveToDBButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (PreviewImage.Source != null)
                {
                    byte[] imagesData;
                    string selectedImagePath = ImagePath.Text.Replace("Путь к фото: ", "");

                    using (FileStream stream = new FileStream(selectedImagePath, FileMode.Open, FileAccess.Read))
                    {
                        BinaryReader reader = new BinaryReader(stream);
                        imagesData = reader.ReadBytes((int)stream.Length);
                    }

                    using (SqlConnection db = new SqlConnection(conn))
                    {
                        string sql = "INSERT INTO Image ([Name], ImageData) VALUES (@Name, @ImageData)";
                        SqlCommand cmd = new SqlCommand(sql, db);
                        cmd.Parameters.AddWithValue("@Name", selectedImagePath);
                        cmd.Parameters.AddWithValue("@ImageData", imagesData);

                        db.Open();
                        cmd.ExecuteNonQuery();
                    }

                    LoadImage();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private void ImageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ImageComboBox.SelectedItem != null)
            {
                ImageInfo selectedImage = (ImageInfo)ImageComboBox.SelectedItem;

                try
                {
                    using (SqlConnection db = new SqlConnection(conn))
                    {
                        string sql = "SELECT ImageData FROM Image WHERE Id = @Id";
                        SqlCommand cmd = new SqlCommand(sql, db);
                        cmd.Parameters.AddWithValue("@Id", selectedImage.ImageID);

                        db.Open();
                        byte[] imageData = (byte[])cmd.ExecuteScalar();

                        using (MemoryStream stream = new MemoryStream(imageData))
                        {
                            BitmapImage image = new BitmapImage();
                            image.BeginInit();
                            image.StreamSource = stream;
                            image.CacheOption = System.Windows.Media.Imaging.BitmapCacheOption.OnLoad;
                            image.EndInit();

                            PreviewImage.Source = image;
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ImageComboBox.SelectedItem != null)
            {
                ImageInfo selectedImage = (ImageInfo)ImageComboBox.SelectedItem;

                try
                {
                    using (SqlConnection db = new SqlConnection(conn))
                    {
                        string sql = "SELECT ImageData FROM Image WHERE Id = @Id";
                        SqlCommand cmd = new SqlCommand(sql, db);
                        cmd.Parameters.AddWithValue("@Id", selectedImage.ImageID);

                        db.Open();
                        byte[] imageData = (byte[])cmd.ExecuteScalar();

                        using (MemoryStream stream = new MemoryStream(imageData))
                        {
                            BitmapImage image = new BitmapImage();
                            image.BeginInit();
                            image.StreamSource = stream;
                            image.CacheOption = System.Windows.Media.Imaging.BitmapCacheOption.OnLoad;
                            image.EndInit();

                            DatabaseImage.Source = image;
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
        }
    }
}

/*create table Image (
Id int not null identity primary key,
[Name] nvarchar(max) not null check([Name]!=''),
ImageData varbinary(max)
)*/
