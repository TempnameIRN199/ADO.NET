using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace hw2
{
    internal class Program
    {
        SqlConnection conn = null;

        public Program()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["MyConn"].ConnectionString;
            conn = new SqlConnection(ConnectionString);
        }

        static void Main(string[] args)
        {
            Program pr = new Program();
            int kuma;
            while (true)
            {

                Console.WriteLine("1. Create table");
                Console.WriteLine("2. Read data");
                Console.WriteLine("3. Insert data");
                kuma = Convert.ToInt32(Console.ReadLine());
                switch (kuma)
                {
                    case 1:
                        pr.CreateTable();
                        break;
                    case 2:
                        pr.ReadDataGroups();
                        break;
                    case 3:
                        pr.InsertGroups();
                        break;
                    default:
                        break;
                }
                if(kuma == 4) {
                    break;
                }
            }
        }

        public void CreateTable()
        {
            SqlCommand cmd = new SqlCommand("create table Groups ([Id] int not null identity primary key, [Name] nvarchar(max) not null check([Name]!=''))", conn);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Table created");
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                    Console.ReadKey();
                }
            }
        }

        public void ReadDataGroups()
        {
            SqlDataReader rdr = null;
            try
            {
                conn.Open();
                Console.WriteLine("Groups:");
                SqlCommand cmd = new SqlCommand("select * from Groups", conn);
                rdr = cmd.ExecuteReader();
                int line = 0;
                while (rdr.Read())
                {
                    if (line == 0)
                    {
                        for (int i = 0; i < rdr.FieldCount; i++)
                        {
                            Console.Write(rdr.GetName(i).ToString() + "\t");
                        }
                    }
                    Console.WriteLine();
                    line++;
                    Console.WriteLine(rdr[0] + "\t" + rdr[1]);
                }
                Console.WriteLine($"Обработано записей {line.ToString()}");
                cmd.Dispose();
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                    Console.ReadKey();
                }
            }
        }

        public void InsertGroups()
        {
            string sql = "insert into Groups (Name) values (@Name)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            Console.WriteLine("Введите название группы:");
            string name = Console.ReadLine();
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
        }
    }
}

