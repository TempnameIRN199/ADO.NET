using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlTypes;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;

namespace lab_3
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

            DateTime dateTime;
            int kuma;
            string specifiedUsername, authorName;


            while (true)
            {
                Console.Write("Введіть завданя - ");
                kuma = Convert.ToInt32(Console.ReadLine());

                switch (kuma) {

                    case 1:
                        pr.ReadDataBook();
                        break;

                    case 2:
                        pr.ReadDataAuthor();
                        break;

                    case 3:
                        Console.Write("Enter Date - ");
                        dateTime = Convert.ToDateTime(Console.ReadLine());
                        pr.FoundBookByDate(dateTime);
                        break;

                    case 4:
                        Console.Write("Enter Name - ");
                        specifiedUsername = Console.ReadLine();
                        pr.ReadPriceBookIsBook(specifiedUsername);
                        break;

                    case 5:
                        Console.Write("Enter Price - ");
                        double specifiedPrice = Convert.ToDouble(Console.ReadLine());
                        Console.Write("Enter Date - ");
                        dateTime = Convert.ToDateTime(Console.ReadLine());
                        pr.ReadBookFromPrice(specifiedPrice, dateTime);
                        break;

                    case 6:
                        pr.ReadBookCount();
                        break;

                    case 7:
                        Console.Write("Enter Author Name - ");
                        authorName = Console.ReadLine();
                        pr.ReadAuthorBooksInfo(authorName);
                        break;

                    default:
                        break;
                }
                if (kuma == 8) { break; }
            }

            
        }

        public void ReadDataBook()
        {
            SqlDataReader rdr = null;

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select c.Name from Clients as c join Books as b on c.Id = b.ClientId where b.Name = 'Война и мир'", conn);
                rdr = cmd.ExecuteReader();
                int line = 0;
                while (rdr.Read())
                {
                    if(line == 0)
                    {
                        for (int i = 0; i < rdr.FieldCount; i++)
                        {
                            Console.Write(rdr.GetName(i).ToString() + '\t');
                        }
                    }
                    Console.WriteLine();
                    line++;
                    Console.WriteLine("{0}", rdr[0]);
                }
                rdr.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
                Console.ReadKey();
            }
        }

        public void ReadDataAuthor()
        {
            SqlDataReader rdr = null;

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select a.Name from Authors as a join Book_Author as ba on a.Id = ba.AuthorId join Books as b on ba.BookId = b.Id where b.Name = 'Война и мир'", conn);
                rdr = cmd.ExecuteReader();
                int line = 0;
                while (rdr.Read())
                {
                    if (line == 0)
                    {
                        for (int i = 0; i < rdr.FieldCount; i++)
                        {
                            Console.Write(rdr.GetName(i).ToString() + '\t');
                        }
                    }
                    Console.WriteLine();
                    line++;
                    Console.WriteLine("{0}", rdr[0]);
                }
                rdr.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
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
                }
                Console.ReadKey();
            }
        }

        public void FoundBookByDate(DateTime dateTime)
        {
            SqlDataReader rdr = null;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select b.Name from Books as b join Clients as c on b.ClientId = c.Id where b.ClientId is not null and c.IsBooks = 1 and b.DateT > @specifiedDate", conn);
                cmd.Parameters.AddWithValue("@specifiedDate", dateTime);
                rdr = cmd.ExecuteReader();
                int line = 0;
                while (rdr.Read())
                {
                    if (line == 0)
                    {
                        for (int i = 0; i < rdr.FieldCount; i++)
                        {
                            Console.Write(rdr.GetName(i).ToString() + '\t');
                        }
                    }
                    Console.WriteLine();
                    line++;
                    Console.WriteLine("{0}", rdr[0]);
                }
                rdr.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
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
                }
                Console.ReadKey();
            }
        }

        public void ReadPriceBookIsBook(string specifiedUsername)
        {
            SqlDataReader rdr = null;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select b.Name, b.Price from Books as b join Clients as c on b.ClientId = c.Id where c.Name = @specifiedUsername", conn);
                cmd.Parameters.AddWithValue("@specifiedUsername", specifiedUsername);
                rdr = cmd.ExecuteReader();
                int line = 0;
                while (rdr.Read())
                {
                    if (line == 0)
                    {
                        for (int i = 0; i < rdr.FieldCount; i++)
                        {
                            Console.Write(rdr.GetName(i).ToString() + '\t');
                        }
                    }
                    Console.WriteLine();
                    line++;
                    Console.WriteLine("{0}\t{1}", rdr[0], rdr[1]);
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                throw;
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
                }
                Console.ReadKey();
            }
        }

        public void ReadBookFromPrice(double specifiedPrice, DateTime dateTime)
        {
            SqlDataReader rdr = null;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select b.Name from Books as b join Clients as c on b.ClientId = c.Id where b.Price > @specifiedPrice and b.DateT > @specifiedDate", conn);
                cmd.Parameters.AddWithValue("@specifiedPrice", specifiedPrice);
                cmd.Parameters.AddWithValue("@specifiedDate", dateTime);
                rdr = cmd.ExecuteReader();
                int line = 0;
                while (rdr.Read())
                {
                    if (line == 0)
                    {
                        for (int i = 0; i < rdr.FieldCount; i++)
                        {
                            Console.Write(rdr.GetName(i).ToString() + '\t');
                        }
                    }
                    Console.WriteLine();
                    line++;
                    Console.WriteLine("{0}", rdr[0]);
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                throw;
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
                }
                Console.ReadKey();
            }
        }

        public void ReadBookCount()
        {
            SqlCommand cmd = null;
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetBookCount", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                int number = (int)cmd.ExecuteScalar();
                Console.WriteLine($"Кількість книг - {number}");
            }
            catch(SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
                Console.ReadKey();
            }
        }

        public void ReadAuthorBooksInfo(string authorName)
        {
            SqlCommand cmd = null;
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetAuthorBooksInfo", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@authorName", authorName);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Console.WriteLine($"Сумма цен всех книг - {rdr[0]}");
                    Console.WriteLine($"Сумма страниц всех книг - {rdr[1]}");
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
                Console.ReadKey();
            }
        }
    }
}