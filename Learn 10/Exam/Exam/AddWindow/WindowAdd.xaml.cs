using Exam.DBContext;
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
using System.Windows.Shapes;

namespace Exam.AddWindow
{
    /// <summary>
    /// Логика взаимодействия для WindowAdd.xaml
    /// </summary>
    public partial class WindowAdd : Window
    {
        DateTime today = DateTime.Today;
        DateTime minDate = DateTime.Today.AddYears(-15);

        public WindowAdd(string title)
        {
            InitializeComponent();
            LoadedGrid(title);
        }

        private void LoadedGrid(string title)
        {
            if (title == "OlympicsAdd")
            {
                this.Title = "OlympicsAdd";

                Label label = new Label();
                label.Content = "Year";
                label.Margin = new Thickness(10);
                Grid.SetRow(label, 0);

                TextBox textBox = new TextBox();
                textBox.Margin = new Thickness(10, 10, -40, 10);
                textBox.Width = 200;
                Grid.SetRow(textBox, 0);

                Label label1 = new Label();
                label1.Content = "Season";
                label1.Margin = new Thickness(10);
                Grid.SetRow(label1, 1);
                
                ComboBox comboBox = new ComboBox();
                comboBox.Margin = new Thickness(10, 10, -40, 10);
                comboBox.Width = 200;
                Grid.SetRow(comboBox, 1);

                Label label2 = new Label();
                label2.Content = "Country";
                label2.Margin = new Thickness(10);
                Grid.SetRow(label2, 2);

                TextBox textBox2 = new TextBox();
                textBox2.Margin = new Thickness(10, 10, -40, 10);
                textBox2.Width = 200;
                Grid.SetRow(textBox2, 2);

                Label label3 = new Label();
                label3.Content = "City";
                label3.Margin = new Thickness(10);
                Grid.SetRow(label3, 3);

                TextBox textBox3 = new TextBox();
                textBox3.Margin = new Thickness(10, 10, -40, 10);
                textBox3.Width = 200;
                Grid.SetRow(textBox3, 3);

                grid.Children.Add(label);
                grid.Children.Add(textBox);
                grid.Children.Add(label1);
                grid.Children.Add(comboBox);
                grid.Children.Add(label2);
                grid.Children.Add(textBox2);
                grid.Children.Add(label3);
                grid.Children.Add(textBox3);

                foreach (var item in Enum.GetValues(typeof(Exam.DBContext.EnumSeason)))
                {
                    comboBox.Items.Add(item);
                }
            }
            else if (title == "SportsAdd")
            {
                this.Title = "SportsAdd";

                Label label = new Label();
                label.Content = "Name";
                label.Margin = new Thickness(10);
                Grid.SetRow(label, 0);

                TextBox textBox = new TextBox();
                textBox.Margin = new Thickness(10, 10, -40, 10);
                textBox.Width = 200;
                Grid.SetRow(textBox, 0);

                Label label1 = new Label();
                label1.Content = "AmountMember";
                label1.Margin = new Thickness(10);
                Grid.SetRow(label1, 1);

                TextBox textBox1 = new TextBox();
                textBox1.Margin = new Thickness(10, 10, -40, 10);
                textBox1.Width = 200;
                Grid.SetRow(textBox1, 1);

                Label label2 = new Label();
                label2.Content = "OlympicId";
                label2.Margin = new Thickness(10);
                Grid.SetRow(label2, 2);

                TextBox textBox2 = new TextBox();
                textBox2.Margin = new Thickness(10, 10, -40, 10);
                textBox2.Width = 200;
                Grid.SetRow(textBox2, 2);


                grid.Children.Add(label);
                grid.Children.Add(textBox);
                grid.Children.Add(label1);
                grid.Children.Add(textBox1);
                grid.Children.Add(label2);
                grid.Children.Add(textBox2);
            }
            else if (title == "MembersAdd")
            {
                this.Title = "MembersAdd";

                Label label = new Label();
                label.Content = "FullName";
                label.Margin = new Thickness(10);
                Grid.SetRow(label, 0);

                TextBox textBox = new TextBox();
                textBox.Margin = new Thickness(10, 10, -40, 10);
                textBox.Width = 200;
                Grid.SetRow(textBox, 0);

                Label label1 = new Label();
                label1.Content = "Country";
                label1.Margin = new Thickness(10);
                Grid.SetRow(label1, 2);

                TextBox textBox1 = new TextBox();
                textBox1.Margin = new Thickness(10, 10, -40, 10);
                textBox1.Width = 200;
                Grid.SetRow(textBox1, 2);

                Label label2 = new Label();
                label2.Content = "DOB";
                label2.Margin = new Thickness(10);
                Grid.SetRow(label2, 1);

                DatePicker datePicker = new DatePicker
                {
                    Name = "datePicker",
                    Margin = new Thickness(10, 10, -40, 10),
                    Width = 200,
                    SelectedDate = minDate,
                    DisplayDateStart = minDate,
                    DisplayDateEnd = today
                };
                Grid.SetRow(datePicker, 1);
                datePicker.SelectedDateChanged += BirthdatePicker_SelectedDateChanged;

                Label label3 = new Label();
                label3.Content = "SportId";
                label3.Margin = new Thickness(10);
                Grid.SetRow(label3, 3);

                TextBox textBox3 = new TextBox();
                textBox3.Margin = new Thickness(10, 10, -40, 10);
                textBox3.Width = 200;
                Grid.SetRow(textBox3, 3);

                grid.Children.Add(label);
                grid.Children.Add(textBox);
                grid.Children.Add(label1);
                grid.Children.Add(textBox1);
                grid.Children.Add(label2);
                grid.Children.Add(datePicker);
                grid.Children.Add(label3);
                grid.Children.Add(textBox3);
            }
            else if (title == "ResultsAdd")
            {
                this.Title = "ResultsAdd";

                Label label = new Label();
                label.Content = "Medal";
                label.Margin = new Thickness(10);
                Grid.SetRow(label, 0);

                ComboBox comboBox = new ComboBox();
                comboBox.Margin = new Thickness(10, 10, -40, 10);
                comboBox.Width = 200;
                Grid.SetRow(comboBox, 0);

                Label label1 = new Label();
                label1.Content = "MemberId";
                label1.Margin = new Thickness(10);
                Grid.SetRow(label1, 1);

                TextBox textBox1 = new TextBox();
                textBox1.Margin = new Thickness(10, 10, -40, 10);
                textBox1.Width = 200;
                Grid.SetRow(textBox1, 1);

                Label label2 = new Label();
                label2.Content = "SportId";
                label2.Margin = new Thickness(10);
                Grid.SetRow(label2, 2);

                TextBox textBox2 = new TextBox();
                textBox2.Margin = new Thickness(10, 10, -40, 10);
                textBox2.Width = 200;
                Grid.SetRow(textBox2, 2);

                grid.Children.Add(label);
                grid.Children.Add(comboBox);
                grid.Children.Add(label1);
                grid.Children.Add(textBox1);
                grid.Children.Add(label2);
                grid.Children.Add(textBox2);

                foreach (var item in Enum.GetValues(typeof(Exam.DBContext.EnuMedal)))
                {
                    comboBox.Items.Add(item);
                }
            }
        }

        private void BirthdatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker datePicker = sender as DatePicker;

            if (datePicker.SelectedDate > minDate)
            {
                datePicker.SelectedDate = minDate;
            }
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using (NintendoContext db = new NintendoContext())
            {
                if (this.Title == "OlympicsAdd")
                {
                    Olympics olympics = new Olympics();
                    olympics.Year = Convert.ToInt32(((TextBox)grid.Children[2]).Text);
                    olympics.Season = (EnumSeason)((ComboBox)grid.Children[4]).SelectedItem;
                    olympics.Country = ((TextBox)grid.Children[6]).Text;
                    olympics.City = ((TextBox)grid.Children[8]).Text;

                    db.Olympics.Add(olympics);
                    db.SaveChanges();
                    MessageBox.Show("Молодець, усе працює");
                    this.Close();
                }
                else if (this.Title == "SportsAdd")
                {
                    Sports sports = new Sports();
                    sports.Name = ((TextBox)grid.Children[2]).Text;
                    sports.AmountMember = Convert.ToInt32(((TextBox)grid.Children[4]).Text);
                    sports.OlympicId = Convert.ToInt32(((TextBox)grid.Children[6]).Text);

                    db.Sports.Add(sports);
                    db.SaveChanges();
                    MessageBox.Show("Молодець, усе працює");
                    this.Close();
                }
                else if (this.Title == "MembersAdd")
                {
                    Members members = new Members();
                    members.FullName = ((TextBox)grid.Children[2]).Text;
                    members.Country = ((TextBox)grid.Children[4]).Text;
                    members.DOB = Convert.ToDateTime(((DatePicker)grid.Children[6]).Text);
                    members.SportId = Convert.ToInt32(((TextBox)grid.Children[8]).Text);

                    db.Members.Add(members);
                    db.SaveChanges();
                    MessageBox.Show("Молодець, усе працює");
                    this.Close();
                }
                else if (this.Title == "ResultsAdd")
                {
                    Results results = new Results();
                    results.Medal = (EnuMedal)((ComboBox)grid.Children[2]).SelectedItem;
                    results.MemberId = Convert.ToInt32(((TextBox)grid.Children[4]).Text);
                    results.SportId = Convert.ToInt32(((TextBox)grid.Children[6]).Text);

                    db.Results.Add(results);
                    db.SaveChanges();
                    MessageBox.Show("Молодець, усе працює");
                    this.Close();
                }
            }
        }

    }
}
