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

namespace lab_8
{
    /// <summary>
    /// Логика взаимодействия для WindowAdd2.xaml
    /// </summary>
    public partial class WindowAdd2 : Window
    {
        TextBox textBox = new TextBox();
        TextBox textBox1 = new TextBox();

        public WindowAdd2(string title, Type type)
        {
            InitializeComponent();
            this.Title = title;
            if (type == typeof(Group))
            {
                Group group = new Group();
                this.DataContext = group;
            }
            else if (type == typeof(Student))
            {
                Student student = new Student();
                this.DataContext = student;
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
                }
            }
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            using (ITStepEntities db = new ITStepEntities())
            {
                if (this.DataContext is Group)
                {
                    if (txtName.Text != "" && txtName.Text != null)
                    {
                        Group group = new Group();
                        group.Name = txtName.Text;
                        db.Groups.Add(group);
                        group.DepartmentId = Convert.ToInt32(textBox.Text);
                        db.Groups.Add(group);
                        group.FormId = Convert.ToInt32(textBox1.Text);
                        db.Groups.Add(group);
                        MessageBox.Show("Отдел добавлен");
                    }
                    else
                    {
                        MessageBox.Show("Введите название отдела");
                        return;
                    }
                }
                else if (this.DataContext is Student)
                {
                    if (txtName.Text != "" && txtName.Text != null)
                    {
                        Student student = new Student();
                        student.Name = txtName.Text;
                        db.Students.Add(student);
                        student.GroupId = Convert.ToInt32(textBox.Text);
                        db.Students.Add(student);
                        student.Email = textBox1.Text;
                        db.Students.Add(student);
                        MessageBox.Show("Студент додан");
                    }
                    else
                    {
                        MessageBox.Show("Введіть дані про студента");
                        return;
                    }
                }
                db.SaveChanges();
            }
            this.Close();
        }
    }
}
