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

namespace lab_8
{
    /// <summary>
    /// Логика взаимодействия для WindowEdit.xaml
    /// </summary>
    public partial class WindowEdit : Window
    {
        Department updDep = new Department();
        Form updForm = new Form();

        public WindowEdit(string title, Type type, Department department1 = null, Form form1 = null)
        {
            InitializeComponent();
            this.Title = title;
            if (type == typeof(Department) && department1 != null)
            {
                this.DataContext = department1;
                updDep = department1;
            }
            else if (type == typeof(Form) && form1 != null)
            {
                this.DataContext = form1;
                updForm = form1;
            }
            LoadData();

        }

        private void LoadData()
        {
            using (ITStepEntities db = new ITStepEntities())
            {
                if (this.DataContext is Department)
                {
                    if (this.Title == "Department")
                    {
                        txtName.Text = updDep.Name;
                    }
                }
                else if (this.DataContext is Form)
                {
                    if (this.Title == "Form")
                    {
                        txtName.Text = updForm.Name;
                    }
                }
            }
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            using (ITStepEntities db = new ITStepEntities())
            {
                if (this.DataContext is Department)
                {
                    if (this.Title == "Department")
                    {
                        updDep = db.Departments.Find(updDep.Id);
                        updDep.Name = txtName.Text;
                        db.SaveChanges();
                        MessageBox.Show("Все вийшло");
                        this.Close();
                    }
                }
                else if (this.DataContext is Form)
                {
                    if (this.Title == "Form")
                    {
                        updForm = db.Forms.Find(updForm.Id);
                        updForm.Name = txtName.Text;
                        db.SaveChanges();
                        MessageBox.Show("Все вийшло");
                        this.Close();
                    }
                }
            }
        }
    }
}
