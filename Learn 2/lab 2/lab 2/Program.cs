using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
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
            string ConnectionString = ConfigurationManager.ConnectionStrings["MyConn"].ConnectionString;
            conn = new SqlConnection(ConnectionString);
        }

        static void Main(string[] args)
        {
            int visitorId, ex;
            Program pr = new Program();

            while(true)
            {
                Console.WriteLine("Введите номер задания");
                ex = Convert.ToInt32(Console.ReadLine());
                switch (ex)
                {
                    case 1:
                        pr.ReadDataDebtorList();
                        break;
                    case 2:
                        pr.ReadDataBooks();
                        break;
                    case 3:
                        pr.ReadDataBookIsBooks();
                        break;
                    case 4:
                        pr.ReadDataClientIsBooks();
                        break;
                    case 5:
                        pr.ReadDataBooksT();
                        break;
                    case 6:
                        pr.DeleteIsBooks();
                        break;
                    case 7:
                        Console.WriteLine("Введите id посетителя: ");
                        visitorId = Convert.ToInt32(Console.ReadLine());
                        pr.BookCount(visitorId);
                        break;
                    case 0:
                        break;
                    default:
                        Console.WriteLine("Нет такого задания");
                        break;
                }
                if(ex == 0)
                {
                    break;
                }
            }
        }

        // 1
        public void ReadDataDebtorList()
        {
            SqlDataReader rdr = null;
            try
            {
                conn.Open();
                Console.WriteLine("Clients:");
                SqlCommand cmd = new SqlCommand("select [Name] from Clients where IsBooks = 1", conn);
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
                    Console.WriteLine(rdr[0] + "\t"/* + rdr[1] + "\t" + rdr[2]*/);
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

        // 2
        public void ReadDataBooks()
        {
            SqlDataReader rdr = null;
            try
            {
                conn.Open();
                Console.WriteLine("Authors: ");
                SqlCommand cmd = new SqlCommand("select a.Name from Authors as a join Book_Author as ba on a.Id = ba.AuthorId join Books as b on ba.BookId = b.Id where b.Id = 3", conn);
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
                    Console.WriteLine(rdr[0] + "\t");
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


        // 3
        public void ReadDataBookIsBooks()
        {
            SqlDataReader rdr = null;
            try
            {
                conn.Open();
                Console.WriteLine("Books:");
                SqlCommand cmd = new SqlCommand("select b.Name from Books as b where b.ClientId is null ", conn);
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
                    Console.WriteLine(rdr[0] + "\t");
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

        // 4
        public void ReadDataClientIsBooks()
        {
            SqlDataReader rdr = null;
            try
            {
                conn.Open();
                Console.WriteLine("Books:");
                SqlCommand cmd = new SqlCommand("select b.Name from Books as b where b.ClientId = 2", conn);
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
                    Console.WriteLine(rdr[0] + "\t");
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

        // 5
        public void ReadDataBooksT()
        {
            SqlDataReader rdr = null;
            try
            {
                conn.Open();
                Console.WriteLine("Books:");
                SqlCommand cmd = new SqlCommand("select b.Name from Books as b where b.DateT >= dateadd(week, -2, getdate())", conn);
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
                    Console.WriteLine(rdr[0] + "\t");
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

        // 6
        public void DeleteIsBooks()
        {
            conn.Open();
            SqlTransaction trans = conn.BeginTransaction();
            try
            {
                SqlCommand updt = new SqlCommand("update Books set ClientId = null where ClientId is not null", conn, trans);
                updt.ExecuteNonQuery();

                SqlCommand updt2 = new SqlCommand("update Clients set IsBooks = ''", conn, trans);
                updt2.ExecuteNonQuery();

                trans.Commit();
                Console.WriteLine("Транзакция выполнена");
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        // 7
        public void BookCount(int visitorId)
        {
            conn.Open();
            // select count(*) as CountBook from Issuances where ClientId = @IdClient and DATEDIFF(year, DateIssuance, getdate()) = 0
            SqlCommand cmd = new SqlCommand("select count(*) as CountBook from Issuances where ClientId = @IdClient and DATEDIFF(year, DateIssuance, getdate()) = 0", conn);
            cmd.Parameters.AddWithValue("@IdClient", visitorId);
            int count = (int)cmd.ExecuteScalar();
            Console.WriteLine($"Количество книг, которые взял посетитель с id {visitorId} в этом году: {count}");
            conn.Close();
        }

        // 
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