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
        Department department1 = new Department();
        Form form1 = new Form();
        Group group1 = new Group();
        Student student1 = new Student();

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
                if (DepartmentsListView.SelectedItems != null)
                {
                    if (DepartmentsListView.SelectedItems.Count == 0)
                    {
                        MessageBox.Show("Виберіть відділ для редагування");
                        return;
                    }
                    else
                    {
                        using (ITStepEntities db = new ITStepEntities())
                        {
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
                                WindowEdit windowEdit = new WindowEdit("Department", typeof(Department), department1, form1);
                                windowEdit.ShowDialog();
                                RefreshDepartmentsDataCustom();
                            }
                        }
                    }
                }
            }
            else if (FormsTabItem.IsSelected)
            {
                if (FormsListView.SelectedItems != null) 
                {
                    if (FormsListView.SelectedItems.Count == 0)
                    {
                        MessageBox.Show("Виберіть форму для редагування");
                        return;
                    }
                    else
                    {
                        using (ITStepEntities db = new ITStepEntities())
                        {
                            foreach (Form form in FormsListView.SelectedItems)
                            {
                                form1 = db.Forms.Find(form.Id);
                            }
                            if (form1 == null)
                            {
                                MessageBox.Show("Не суй туди руки");
                                return;
                            }
                            else
                            {
                                WindowEdit windowEdit = new WindowEdit("Form", typeof(Form), department1, form1);
                                windowEdit.ShowDialog();
                                RefreshDepartmentsDataCustom();
                            }
                        }
                    }
                }
            }
            else if (GroupsTabItem.IsSelected)
            {
                if (GroupsListView.SelectedItems != null)
                {
                    if (GroupsListView.SelectedItems.Count == 0)
                    {
                        MessageBox.Show("Виберіть форму для редагування");
                        return;
                    }
                    else
                    {
                        using (ITStepEntities db = new ITStepEntities())
                        {
                            foreach (Group group in GroupsListView.SelectedItems)
                            {
                                group1 = db.Groups.Find(group.Id);
                            }
                            if (form1 == null)
                            {
                                MessageBox.Show("Не суй туди руки");
                                return;
                            }
                            else
                            {
                                WindowEdit2 windowEdit2 = new WindowEdit2("Group", typeof(Group), group1, student1);
                                windowEdit2.ShowDialog();
                                RefreshDepartmentsDataCustom();
                            }
                        }
                    }
                }
            }
            else if (StudentsTabItem.IsSelected)
            {
                if (StudentsListView.SelectedItems != null)
                {
                    if (StudentsListView.SelectedItems.Count == 0)
                    {
                        MessageBox.Show("Виберіть форму для редагування");
                        return;
                    }
                    else
                    {
                        using (ITStepEntities db = new ITStepEntities())
                        {
                            foreach (Student student in StudentsListView.SelectedItems)
                            {
                                student1 = db.Students.Find(student.Id);
                            }
                            if (form1 == null)
                            {
                                MessageBox.Show("Не суй туди руки");
                                return;
                            }
                            else
                            {
                                WindowEdit2 windowEdit2 = new WindowEdit2("Student", typeof(Student), group1, student1);
                                windowEdit2.ShowDialog();
                                RefreshDepartmentsDataCustom();
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите элемент для редактирования");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (DepartmentsTabItem.IsSelected)
            {
                if (DepartmentsListView.SelectedItems != null)
                {
                    if (DepartmentsListView.SelectedItems.Count == 0)
                    {
                        MessageBox.Show("Виберіть відділ для видалення");
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
            else if (FormsTabItem.IsSelected)
            {
                if (FormsListView.SelectedItems != null)
                {
                    if (FormsListView.SelectedItems.Count == 0)
                    {
                        MessageBox.Show("Виберіть форму для видалення");
                        return;
                    }
                    else
                    {
                        using (ITStepEntities db = new ITStepEntities())
                        {
                            Form form1 = new Form();
                            foreach (Form form in FormsListView.SelectedItems)
                            {
                                form1 = db.Forms.Find(form.Id);
                            }
                            if (form1 == null)
                            {
                                MessageBox.Show("Не суй туди руки");
                                return;
                            }
                            else
                            {
                                db.Forms.Remove(form1);
                                db.SaveChanges();
                                RefreshDepartmentsDataCustom();
                            }
                        }
                    }
                }
            }
            else if (GroupsTabItem.IsSelected)
            {
                if (GroupsListView.SelectedItems != null)
                {
                    if (GroupsListView.SelectedItems.Count == 0)
                    {
                        MessageBox.Show("Виберіть форму для видалення");
                        return;
                    }
                    else
                    {
                        using (ITStepEntities db = new ITStepEntities())
                        {
                            Group group1 = new Group();
                            foreach (Group group in GroupsListView.SelectedItems)
                            {
                                group1 = db.Groups.Find(group.Id);
                            }
                            if (group1 == null)
                            {
                                MessageBox.Show("Не суй туди руки");
                                return;
                            }
                            else
                            {
                                db.Groups.Remove(group1);
                                db.SaveChanges();
                                RefreshDepartmentsDataCustom();
                            }
                        }
                    }
                }
            }
            else if (StudentsTabItem.IsSelected)
            {
                if (StudentsListView.SelectedItems != null)
                {
                    if (StudentsListView.SelectedItems.Count == 0)
                    {
                        MessageBox.Show("Виберіть форму для видалення");
                        return;
                    }
                    else
                    {
                        using (ITStepEntities db = new ITStepEntities())
                        {
                            Student student1 = new Student();
                            foreach (Student student in StudentsListView.SelectedItems)
                            {
                                student1 = db.Students.Find(student.Id);
                            }
                            if (student1 == null)
                            {
                                MessageBox.Show("Не суй туди руки");
                                return;
                            }
                            else
                            {
                                db.Students.Remove(student1);
                                db.SaveChanges();
                                RefreshDepartmentsDataCustom();
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите элемент для удаления");
            }
        }
    }
}

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
