using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace lab_2
{
    internal class Program
    {
        SqlConnection conn = null;

        public Program()
        {
            conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=DESKTOP-RACOKG7;Initial Catalog=Library;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }

        static void Main(string[] args)
        {
            Program pr = new Program();
            pr.ReadDataClients();
            pr.ReadDataBooks();
            pr.ReadDataAuthors();
        }

        public void ReadDataClients()
        {
            SqlDataReader rdr = null;
            try
            {
                conn.Open();
                Console.WriteLine("Clients:");
                SqlCommand cmd = new SqlCommand("select * from Clients", conn);
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
                    Console.WriteLine(rdr[0] + "\t" + rdr[1] + "\t" + rdr[2]);
                }
                Console.WriteLine($"Обработано записей {line.ToString()}");
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

        public void ReadDataBooks()
        {
            SqlDataReader rdr = null;
            try
            {
                conn.Open();
                Console.WriteLine("Books:");
                SqlCommand cmd = new SqlCommand("select * from Books", conn);
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
                    Console.WriteLine(rdr[0] + "\t" + rdr[1] + "\t" + rdr[2]);
                }
                Console.WriteLine($"Обработано записей {line.ToString()}");
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

        public void ReadDataAuthors()
        {
            SqlDataReader rdr = null;
            try
            {
                conn.Open();
                Console.WriteLine("Authors:");
                SqlCommand cmd = new SqlCommand("select * from Authors", conn);
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
    }
}



//create database Library
//use Library

//create table Clients (
//Id int not null identity primary key,
//[Name] nvarchar(max) not null check([Name]!= ''),
//IsBooks bit not null default(0)
//)

//create table Books (
//Id int not null identity primary key,
//[Name] nvarchar(max) not null check([Name]!= ''),
//ClientId int null foreign key references Clients(Id)
//)

//create table Authors (
//Id int not null identity primary key,
//[Name] nvarchar(max) not null check([Name]!= '')
//)

//create table Book_Author (
//BookId int not null foreign key references Books(Id),
//AuthorId int not null foreign key references Authors(Id)
//)