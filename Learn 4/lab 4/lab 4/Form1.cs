using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.Common;
using System.Xml.Linq;

namespace lab_4
{
    public partial class Form1 : Form
    {
        private static string connetionString = ConfigurationManager.ConnectionStrings["MyConn"].ConnectionString;

        private SqlDataAdapter adapter;
        private DataTable dataTable;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connetionString))
            {
                try
                {
                    conn.Open();
                    DataTable dt = conn.GetSchema("Tables");
                    foreach (DataRow row in dt.Rows)
                    {
                        string tablename = (string)row[2];
                        comboBox1.Items.Add(tablename);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedTable = comboBox1.SelectedItem.ToString();
            TableData(selectedTable);
        }

        private void TableData(string tableName)
        {
            using (SqlConnection conn = new SqlConnection(connetionString))
            {
                try
                {
                    conn.Open();
                    string sql = $"SELECT * FROM {tableName}";
                    adapter = new SqlDataAdapter(sql, conn);
                    dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }
        }
    }
}
//
//create database Sales
//use Sales
//
//create table Buyers (
//Id int not null identity primary key,
//[Name] nvarchar(max) not null check([Name] != ''),
//Surname nvarchar(max) not null check(Surname!='')
//)
//
//create table Sellers (
//Id int not null identity primary key,
//[Name] nvarchar(max) not null check([Name] != ''),
//Surname nvarchar(max) not null check(Surname!='')
//)
//
//create table Sales (
//Id int not null identity primary key,
//BuyerId int not null foreign key references Buyers(Id),
//SellerId int not null foreign key references Sellers(Id),
//SaleSum money not null,
//SaleDate date not null
//)
//