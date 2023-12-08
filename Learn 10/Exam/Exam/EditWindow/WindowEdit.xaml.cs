using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
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
using Exam.DBContext;

namespace Exam.EditWindow
{
    /// <summary>
    /// Логика взаимодействия для WindowEdit.xaml
    /// </summary>
    public partial class WindowEdit : Window
    {
        DateTime today = DateTime.Today;
        DateTime minDate = DateTime.Today.AddYears(-15);

        Olympics updOl = new Olympics();
        Sports updSp = new Sports();
        Members updMm = new Members();
        Results updRs = new Results();

        public WindowEdit(string title, Type type, Olympics olympics1 = null, Sports sports1 = null, Members members1 = null, Results results1 = null)
        {
            InitializeComponent();
            if (type == typeof(Olympics) && olympics1 != null)
            {
                updOl = olympics1;
                LoadedGrid(title, type);
            }
            else if (type == typeof(Sports) && sports1 != null)
            {
                updSp = sports1;
                LoadedGrid(title, type);
            }
            else if (type == typeof(Members) && members1 != null)
            {
                updMm = members1;
                LoadedGrid(title, type);
            }
            else if (type == typeof(Results) && results1 != null)
            {
                updRs = results1;
                LoadedGrid(title, type);
            }
        }

        private void LoadedGrid(string title, Type type)
        {
            if (title == "OlympicsEdit" && type == typeof(Olympics))
            {
                this.Title = "OlympicsEdit";

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

                textBox.Text = updOl.Year.ToString();
                comboBox.SelectedItem = updOl.Season;
                textBox2.Text = updOl.Country;
                textBox3.Text = updOl.City;
            }
            else if (title == "SportsEdit" && type == typeof(Sports))
            {
                this.Title = "SportsEdit";

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

                textBox.Text = updSp.Name;
                textBox1.Text = updSp.AmountMember.ToString();
                textBox2.Text = updSp.OlympicId.ToString();
            }
            else if (title == "MembersEdit" && type == typeof(Members))
            {
                this.Title = "MembersEdit";

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

                textBox.Text = updMm.FullName;
                textBox1.Text = updMm.Country;
                datePicker.SelectedDate = updMm.DOB;
                textBox3.Text = updMm.SportId.ToString();
            }
            else if (title == "ResultsEdit" && type == typeof(Results))
            {
                this.Title = "ResultsEdit";

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

                comboBox.SelectedItem = updRs.Medal;
                textBox1.Text = updRs.MemberId.ToString();
                textBox2.Text = updRs.SportId.ToString();
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
                if (this.Title == "OlympicsEdit")
                {
                    updOl.Year = Convert.ToInt32((grid.Children[2] as TextBox).Text);
                    updOl.Season = (EnumSeason)(grid.Children[4] as ComboBox).SelectedItem;
                    updOl.Country = (grid.Children[6] as TextBox).Text;
                    updOl.City = (grid.Children[8] as TextBox).Text;

                    var existingOlympics = db.Olympics.AsNoTracking().FirstOrDefault(o => o.Id == updOl.Id);

                    if (existingOlympics != null)
                    {
                        existingOlympics.Year = updOl.Year;
                        existingOlympics.Season = updOl.Season;
                        existingOlympics.Country = updOl.Country;
                        existingOlympics.City = updOl.City;

                        db.Entry(existingOlympics).State = EntityState.Modified;
                        db.SaveChanges();
                        MessageBox.Show("Изменения сохранены");
                        this.Close();
                    }
                }
                else if (this.Title == "SportsEdit")
                {
                    updSp.Name = (grid.Children[2] as TextBox).Text;
                    updSp.AmountMember = Convert.ToInt32((grid.Children[4] as TextBox).Text);
                    updSp.OlympicId = Convert.ToInt32((grid.Children[6] as TextBox).Text);

                    var existingSports = db.Sports.AsNoTracking().FirstOrDefault(o => o.Id == updSp.Id);

                    if (existingSports != null)
                    {
                        existingSports.Name = updSp.Name;
                        existingSports.AmountMember = updSp.AmountMember;
                        existingSports.OlympicId = updSp.OlympicId;

                        db.Entry(existingSports).State = EntityState.Modified;
                        db.SaveChanges();
                        MessageBox.Show("Изменения сохранены");
                        this.Close();
                    }
                }
                else if (this.Title == "MembersEdit")
                {
                    updMm.FullName = (grid.Children[2] as TextBox).Text;
                    updMm.Country = (grid.Children[4] as TextBox).Text;
                    updMm.DOB = Convert.ToDateTime((grid.Children[6] as DatePicker).Text);
                    updMm.SportId = Convert.ToInt32((grid.Children[8] as TextBox).Text);

                    var existingMembers = db.Members.AsNoTracking().FirstOrDefault(o => o.Id == updMm.Id);

                    if (existingMembers != null)
                    {
                        existingMembers.FullName = updMm.FullName;
                        existingMembers.Country = updMm.Country;
                        existingMembers.DOB = updMm.DOB;
                        existingMembers.SportId = updMm.SportId;

                        db.Entry(existingMembers).State = EntityState.Modified;
                        db.SaveChanges();
                        MessageBox.Show("Изменения сохранены");
                        this.Close();
                    }
                }
                else if (this.Title == "ResultsEdit")
                {
                    updRs.Medal = (EnuMedal)(grid.Children[2] as ComboBox).SelectedItem;
                    updRs.MemberId = Convert.ToInt32((grid.Children[4] as TextBox).Text);
                    updRs.SportId = Convert.ToInt32((grid.Children[6] as TextBox).Text);

                    var existingResults = db.Results.AsNoTracking().FirstOrDefault(o => o.Id == updRs.Id);

                    if (existingResults != null)
                    {
                        existingResults.Medal = updRs.Medal;
                        existingResults.MemberId = updRs.MemberId;
                        existingResults.SportId = updRs.SportId;

                        db.Entry(existingResults).State = EntityState.Modified;
                        db.SaveChanges();
                        MessageBox.Show("Изменения сохранены");
                        this.Close();
                    }
                }
            }
        }
    }
}
