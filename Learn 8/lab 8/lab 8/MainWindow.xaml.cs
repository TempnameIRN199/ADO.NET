using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab_8
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RefreshDepartmentsData(object sender, RoutedEventArgs e)
        {
            using (ITStepEntities db = new ITStepEntities())
            {
                DepartmentsListView.ItemsSource = null;
                FormsListView.ItemsSource = null;
                GroupsListView.ItemsSource = null;
                StudentsListView.ItemsSource = null;

                DepartmentsListView.ItemsSource = db.Departments.ToList();
                FormsListView.ItemsSource = db.Forms.ToList();
                GroupsListView.ItemsSource = db.Groups.ToList();
                StudentsListView.ItemsSource = db.Students.ToList();
            }
        }

        private void RefreshDepartmentsDataCustom()
        {
            using (ITStepEntities db = new ITStepEntities())
            {
                DepartmentsListView.ItemsSource = null;
                FormsListView.ItemsSource = null;
                GroupsListView.ItemsSource = null;
                StudentsListView.ItemsSource = null;

                DepartmentsListView.ItemsSource = db.Departments.ToList();
                FormsListView.ItemsSource = db.Forms.ToList();
                GroupsListView.ItemsSource = db.Groups.ToList();
                StudentsListView.ItemsSource = db.Students.ToList();
            }
        }

        private void OpenAddForm(string title, Type type)
        {
            WindowAdd windowAdd = new WindowAdd(title, type);
            windowAdd.ShowDialog();

            
        }

        private void OpenAddForm2(string title, Type type)
        {
            WindowAdd2 windowAdd2 = new WindowAdd2(title, type);
            windowAdd2.ShowDialog();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (DepartmentsTabItem.IsSelected)
            {
                OpenAddForm("DepartmentAdd", typeof(Department));
            }
            else if (FormsTabItem.IsSelected)
            {
                OpenAddForm("Form", typeof(Form));
            }
            else if (GroupsTabItem.IsSelected)
            {
                OpenAddForm2("Group", typeof(Group));
            }
            else if (StudentsTabItem.IsSelected)
            {
                OpenAddForm2("Student", typeof(Student));
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (DepartmentsTabItem.IsSelected)
            {
                OpenAddForm("DepartmentEdit", typeof(Department));
            }
            else if (FormsTabItem.IsSelected)
            {
                OpenAddForm("Form", typeof(Form));
            }
            else if (GroupsTabItem.IsSelected)
            {
                OpenAddForm2("Group", typeof(Group));
            }
            else if (StudentsTabItem.IsSelected)
            {
                OpenAddForm2("Student", typeof(Student));
            }
            else
            {
                MessageBox.Show("Выберите элемент для редактирования");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (DepartmentsListView.SelectedItems != null)
            {
                if (DepartmentsListView.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Выберите товар для удаления");
                    return;
                }
                else
                {
                    using (ITStepEntities db = new ITStepEntities())
                    {
                        Department department1 = new Department();
                        foreach (Department department in DepartmentsListView.SelectedItems)
                        {
                            department1 = db.Departments.Find(department.Id);
                        }
                        if (department1 == null)
                        {
                            MessageBox.Show("Не суй туди руки");
                            return;
                        }
                        else
                        {
                            db.Departments.Remove(department1);
                            db.SaveChanges();
                            RefreshDepartmentsDataCustom();
                        }
                    }
                }
            }
        }

        
    }
}
// 

/*using (ITStepEntities db = new ITStepEntities())
                    {
                        foreach (Department department in DepartmentsListView.SelectedItems)
                        {
                            Department department1 = db.Departments.Find(department.Id);
                            if (department1 == null)
                            {
                                MessageBox.Show("Не суй туди руки");
                                return;
                            }
                            else
                            {
                                db.Departments.Remove(department1);
                                db.SaveChanges();
                            }
                        }
                    }*/

/*create database ITStep
use ITStep

create table Departments (
Id int not null identity primary key,
[Name] nvarchar(max) not null check([Name]!='')
)

create table Forms (
Id int not null identity primary key,
[Name] nvarchar(max) not null check([Name]!='')
)

create table Groups (
Id int not null identity primary key,
[Name] nvarchar(max) not null check([Name]!=''),
DepartmentId int not null foreign key references Departments(Id),
FormId int not null foreign key references Forms(Id)
)

create table Students (
Id int not null identity primary key,
[Name] nvarchar(max) not null check([Name]!=''),
GroupId int not null foreign key references Groups(Id),
Email nvarchar(max)
)*/
