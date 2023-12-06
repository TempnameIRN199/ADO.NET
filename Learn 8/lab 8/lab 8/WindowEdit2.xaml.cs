using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace lab_8
{
    /// <summary>
    /// Логика взаимодействия для WindowEdit2.xaml
    /// </summary>
    public partial class WindowEdit2 : Window
    {
        TextBox textBox = new TextBox();
        TextBox textBox1 = new TextBox();

        Group updGroup = new Group();
        Student updStudent = new Student();

        public WindowEdit2(string title, Type type, Group group1, Student student1)
        {
            InitializeComponent();
            this.Title = title;
            if (type == typeof(Group) && updGroup != null)
            {
                this.DataContext = group1;
                updGroup = group1;
            }
            else if (type == typeof(Student) && updStudent != null)
            {
                this.DataContext = student1;
                updStudent = student1;
            }
            LoadWindow(type);
        }

        private void LoadWindow(Type type)
        {
            using (ITStepEntities db = new ITStepEntities())
            {
                if (this.DataContext is Group)
                {
                    Label label = new Label();
                    label.Content = "DepartmentId";
                    label.Margin = new Thickness(10);
                    Grid.SetRow(label, 1);

                    Grid.SetRow(textBox, 1);
                    textBox.Margin = new Thickness(10, 10, -40, 10);
                    textBox.Width = 200;

                    Label label1 = new Label();
                    label1.Content = "FormId";
                    label1.Margin = new Thickness(10);
                    Grid.SetRow(label1, 2);

                    Grid.SetRow(textBox1, 2);
                    textBox1.Margin = new Thickness(10, 10, -40, 10);
                    textBox1.Width = 200;

                    grid.Children.Add(label);
                    grid.Children.Add(textBox);
                    grid.Children.Add(label1);
                    grid.Children.Add(textBox1);

                    if (this.Title == "Group")
                    {
                        txtName.Text = updGroup.Name;
                        textBox.Text = updGroup.DepartmentId.ToString();
                        textBox1.Text = updGroup.FormId.ToString();
                    }
                }
                if (this.DataContext is Student)
                {
                    Label label = new Label();
                    label.Content = "GroupId";
                    label.Margin = new Thickness(10);
                    Grid.SetRow(label, 1);

                    Grid.SetRow(textBox, 1);
                    textBox.Margin = new Thickness(10, 10, -40, 10);
                    textBox.Width = 200;

                    Label label1 = new Label();
                    label1.Content = "Email";
                    label1.Margin = new Thickness(10);
                    Grid.SetRow(label1, 2);

                    Grid.SetRow(textBox1, 2);
                    textBox1.Margin = new Thickness(10, 10, -40, 10);
                    textBox1.Width = 200;

                    grid.Children.Add(label);
                    grid.Children.Add(textBox);
                    grid.Children.Add(label1);
                    grid.Children.Add(textBox1);

                    if (this.Title == "Student")
                    {
                        txtName.Text = updStudent.Name;
                        textBox.Text = updStudent.GroupId.ToString();
                        textBox1.Text = updStudent.Email;
                    }
                }
            }
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            using (ITStepEntities db = new ITStepEntities())
            {
                if (this.DataContext is Group)
                {
                    if (this.Title == "Group")
                    {
                        updGroup = db.Groups.Find(updGroup.Id);
                        updGroup.Name = txtName.Text;
                        updGroup.DepartmentId = Convert.ToInt32(textBox.Text);
                        updGroup.FormId = Convert.ToInt32(textBox1.Text);
                        db.SaveChanges();
                        MessageBox.Show("Все вийшло");
                        this.Close();
                    }
                }
                else if (this.DataContext is Student)
                {
                    if (this.Title == "Student")
                    {
                        updStudent = db.Students.Find(updStudent.Id);
                        updStudent.Name = txtName.Text;
                        updStudent.GroupId = Convert.ToInt32(textBox.Text);
                        updStudent.Email = textBox1.Text;
                        db.SaveChanges();
                        MessageBox.Show("Все вийшло");
                        this.Close();
                    }
                }
            }
        }
    }
}
