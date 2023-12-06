using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
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
using System.Data.Entity;
using System.Data;

namespace lab_8
{
    /// <summary>
    /// Логика взаимодействия для WindowAdd.xaml
    /// </summary>
    public partial class WindowAdd : Window
    {

        public WindowAdd(string title, Type type)
        {
            InitializeComponent();
            this.Title = title;
            if (type == typeof(Department))
            {
                Department department = new Department();
                this.DataContext = department;

                if (this.Title == "DepartmentEdit")
                {
                    using (ITStepEntities db = new ITStepEntities())
                    {
                        txtName.Text = department1.Name;
                    }
                }
                if (this.Title == "DepartmentAdd")
                {
                    txtName.Text = "";
                }
            }
            else if (type == typeof(Form))
            {
                Form form = new Form();
                this.DataContext = form;
            }
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            using (ITStepEntities db = new ITStepEntities())
            {
                if (this.DataContext is Department)
                {
                    if(this.Title == "DepartmentAdd")
                    {
                        if (txtName.Text != "" && txtName.Text != null)
                        {
                            Department department = new Department();
                            department.Name = txtName.Text;
                            db.Departments.Add(department);
                            MessageBox.Show("Отдел добавлен");
                        }
                        else
                        {
                            MessageBox.Show("Введите название отдела");
                            return;
                        }
                    }
                    else if (this.Title == "DepartmentEdit")
                    {
                        Department department = db.Departments.Find(((Department)this.DataContext).Id);
                        if (txtName.Text != "" && txtName.Text != null)
                        {
                            department.Name = txtName.Text;
                            MessageBox.Show("Отдел изменен");
                        }
                        else
                        {
                            MessageBox.Show("Введите название отдела");
                            return;
                        }
                    }
                    
                }
                else if (this.DataContext is Form)
                {
                    if (txtName.Text != "" && txtName.Text != null)
                    {
                        Form form = new Form();
                        form.Name = txtName.Text;
                        db.Forms.Add(form);
                        MessageBox.Show("Форма обучения добавлена");
                    }
                    else
                    {
                        MessageBox.Show("Введите название формы обучения");
                        return;
                    }
                }
                db.SaveChanges();
            }
            this.Close();
        }
    }
}
