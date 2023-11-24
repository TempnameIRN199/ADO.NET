using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;
using System.Runtime.CompilerServices;

namespace hw_4
{
    public partial class Form1 : Form
    {
        private Data daTa;
        private DataTable dtStudents;
        private DataTable dtTeachers;
        private DataTable dtClassrooms;
        private DataTable dtSubjects;

        public Form1()
        {
            InitializeComponent();
            daTa = new Data();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dtStudents = daTa.GetStudents();
            dataGridView1.DataSource = dtStudents;
            dtTeachers = daTa.GetTeachers();
            dataGridView2.DataSource = dtTeachers;
            dtClassrooms = daTa.GetClassrooms();
            dataGridView3.DataSource = dtClassrooms;
            dtSubjects = daTa.GetSubjects();
            dataGridView4.DataSource = dtSubjects;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dtStudents = daTa.GetStudents();
            dataGridView1.DataSource = dtStudents;
            dtTeachers = daTa.GetTeachers();
            dataGridView2.DataSource = dtTeachers;
            dtClassrooms = daTa.GetClassrooms();
            dataGridView3.DataSource = dtClassrooms;
            dtSubjects = daTa.GetSubjects();
            dataGridView4.DataSource = dtSubjects;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            daTa.UpdateStudent(dtStudents);
            daTa.UpdateTeacher(dtTeachers);
            daTa.UpdateClassroom(dtClassrooms);
            daTa.UpdateSubject(dtSubjects);

            MessageBox.Show("Changes saved successfully!");
        }
    }
}
//
//create database Nau
//use Nau
//
//create table Students (
//Id int not null identity primary key,
//[Name] nvarchar(max) not null check([Name] != ''),
//Age int not null
//)
//
//create table Teachers (
//Id int not null identity primary key,
//[Name] nvarchar(max) not null check([Name] != ''),
//Department nvarchar(max) not null
//)
//
//create table Classrooms (
//Id int not null identity primary key,
//[Name] nvarchar(max) not null check([Name] != ''),
//Capacity int not null
//)
//
//create table Subjects (
//Id int not null identity primary key,
//[Name] nvarchar(max) not null check([Name] != ''),
//TeachearId int foreign key references Teachers(Id),
//ClassroomId int foreign key references Classrooms(Id)
//)
//