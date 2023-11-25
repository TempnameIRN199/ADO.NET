using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab_5
{
    public partial class Form1 : Form
    {

        private string conn = ConfigurationManager.ConnectionStrings["MyConn"].ConnectionString;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = textBox1.Text;
            using (SqlConnection db = new SqlConnection(conn))
            {
                try
                {
                    DataTable dataTable = ExecuteQuery(sql);
                    switch (tabControl1.SelectedIndex)
                    {
                        case 0:
                            ListView_2d(dataTable, listView1);
                            break;
                        case 1:
                            ListView_2d(dataTable, listView2);
                            break;
                        case 2:
                            ListView_2d(dataTable, listView3);
                            break;
                        case 3:
                            ListView_2d(dataTable, listView4);
                            break;
                        case 4:
                            ListView_2d(dataTable, listView5);
                            break;
                        case 5:
                            ListView_2d(dataTable, listView6);
                            break;
                        default:
                            break;
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                    throw;
                }
                finally
                {
                    db.Close();
                }
            }
        }

        private void ListView_2d(DataTable dataTable, ListView listView)
        {
            listView.Clear();
            listView.View = View.Details;
            foreach (DataColumn column in dataTable.Columns)
            {
                listView.Columns.Add(column.ColumnName);
            }
            foreach (DataRow row in dataTable.Rows)
            {
                ListViewItem item = new ListViewItem(row[0].ToString());
                for (int i = 1; i < dataTable.Columns.Count; i++)
                {
                    item.SubItems.Add(row[i].ToString());
                }
                listView.Items.Add(item);
            }
        }

        private DataTable ExecuteQuery(string sql)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection db = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand(sql, db);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dataTable);
            }
            return dataTable;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sql = textBox1.Text;
            using (SqlConnection db = new SqlConnection(conn))
            {
                try
                {
                    DataTable dataTable = ExecuteQuery(sql);
                    switch (tabControl1.SelectedIndex)
                    {
                        case 0:
                            ListView_1d(listView1);
                            break;
                        case 1:
                            ListView_1d(listView2);
                            break;
                        case 2:
                            ListView_1d(listView3);
                            break;
                        case 3:
                            ListView_1d(listView4);
                            break;
                        case 4:
                            ListView_1d(listView5);
                            break;
                        case 5:
                            ListView_1d(listView6);
                            break;
                        default:
                            break;
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                    throw;
                }
                finally
                {
                    db.Close();
                }
            }
            
        }

        private void ListView_1d(ListView listView)
        {
            if (listView.View == View.Details)
            {
                listView.View = View.List;
            }
            else
            {
                listView.View = View.Details;
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    listView1.Clear();
                    break;
                case 1:
                    listView2.Clear();
                    break;
                case 2:
                    listView3.Clear();
                    break;
                case 3:
                    listView4.Clear();
                    break;
                case 4:
                    listView5.Clear();
                    break;
                case 5:
                    listView6.Clear();
                    break;
                default:
                    break;
            }
        }
    }
}
