using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataSet shopDB = new DataSet("ShopDB");
            DataTable sales = new DataTable("Sales");

            DataTable customers = new DataTable("Customers");

            DataTable sellers = new DataTable("Sellers");
            shopDB.Tables.Add(sellers);

            DataColumn IdSellers = new DataColumn("Id", Type.GetType("System.Int32"));
            IdSellers.Unique = true;
            IdSellers.AllowDBNull = false;
            IdSellers.AutoIncrement = true;
            IdSellers.AutoIncrementSeed = 1;
            IdSellers.AutoIncrementStep = 1;

            DataColumn FirstNameSellers = new DataColumn("FirstName", Type.GetType("System.String"));
            FirstNameSellers.AllowDBNull = false;

            DataColumn LastNameSellers = new DataColumn("LastName", Type.GetType("System.String"));
            LastNameSellers.AllowDBNull = false;

            sellers.Columns.Add(IdSellers);
            sellers.Columns.Add(FirstNameSellers);
            sellers.Columns.Add(LastNameSellers);

            sellers.PrimaryKey = new DataColumn[] { sellers.Columns["Id"] };
            shopDB.Tables.Add(sales);
            Console.WriteLine("Sellers complete");

            DataRow seller1 = sellers.NewRow();
            seller1.ItemArray = new object[] { null, "kuma1", "hentuho1" };
            sellers.Rows.Add(seller1);
            sellers.Rows.Add(new object[] { null, "kuma2", "hentuho2" });
            sellers.Rows.Add(new object[] { null, "kuma3", "hentuho3" });
            sellers.Rows.Add(new object[] { null, "kuma4", "hentuho4" });

            Console.Write("\tId \tFirstName \tLastName");
            Console.WriteLine();
            foreach (DataRow item in sellers.Rows)
            {
                foreach (var cell in item.ItemArray)
                {
                    Console.Write("\t{0}", cell);
                }
                Console.WriteLine();
            }

            DataColumn IdCustomers = new DataColumn("Id", Type.GetType("System.Int32"));
            IdCustomers.Unique = true;
            IdCustomers.AllowDBNull = false;
            IdCustomers.AutoIncrement = true;
            IdCustomers.AutoIncrementSeed = 1;
            IdCustomers.AutoIncrementStep = 1;

            DataColumn FirstNameCustomers = new DataColumn("FirstName", Type.GetType("System.String"));
            FirstNameCustomers.AllowDBNull = false;

            DataColumn LastNameCustomers = new DataColumn("LastName", Type.GetType("System.String"));
            LastNameCustomers.AllowDBNull = false;

            customers.PrimaryKey = new DataColumn[] { customers.Columns["Id"] };
            shopDB.Tables.Add(customers);
            Console.WriteLine("Customers complete");

            customers.Columns.Add(IdCustomers);
            customers.Columns.Add(FirstNameCustomers);
            customers.Columns.Add(LastNameCustomers);

            DataRow customer1 = customers.NewRow();
            customer1.ItemArray = new object[] { null, "kuma1", "hentuho1" };
            customers.Rows.Add(customer1);
            customers.Rows.Add(new object[] { null, "kuma2", "hentuho2" });
            customers.Rows.Add(new object[] { null, "kuma3", "hentuho3" });
            customers.Rows.Add(new object[] { null, "kuma4", "hentuho4" });

            Console.Write("\tId \tFirstName \tLastName");
            Console.WriteLine();
            foreach (DataRow item in customers.Rows)
            {
                foreach (var cell in item.ItemArray)
                {
                    Console.Write("\t{0}", cell);
                }
                Console.WriteLine();
            }

            DataColumn IdSales = new DataColumn("Id", Type.GetType("System.Int32"));
            IdSales.Unique = true;
            IdSales.AllowDBNull = false;
            IdSales.AutoIncrement = true;
            IdSales.AutoIncrementSeed = 1;
            IdSales.AutoIncrementStep = 1;

            DataColumn SellerId = new DataColumn("SellerId", Type.GetType("System.Int32"));
            SellerId.AllowDBNull = false;

            /*
            ForeignKeyConstraint fkSeller = new ForeignKeyConstraint("FK_SellerId", shopDB.Tables["Sellers"].Columns["Id"], shopDB.Tables["Sales"].Columns["IdSeller"]);
            shopDB.Tables["Sales"].Constraints.Add(fkSeller);*/

            DataColumn CustomerId = new DataColumn("CustomerId", Type.GetType("System.Int32"));
            CustomerId.AllowDBNull = false;
            /*ForeignKeyConstraint fkCustomer = new ForeignKeyConstraint("FK_CustomerId", shopDB.Tables["Customers"].Columns["Id"], shopDB.Tables["Sales"].Columns["IdCustomer"]);
            shopDB.Tables["Sales"].Constraints.Add(fkCustomer);*/

            sales.Columns.Add("DateofSale", Type.GetType("System.DateTime"));
            sales.Columns["DateofSale"].AllowDBNull = false;

            sales.Columns.Add("Price", Type.GetType("System.Decimal"));
            sales.Columns["Price"].AllowDBNull = false;

            sales.PrimaryKey = new DataColumn[] { sales.Columns["Id"] };

            ForeignKeyConstraint foreignKeyConstraint = new ForeignKeyConstraint("FK_SellerId", shopDB.Tables["Sellers"].Columns["Id"], shopDB.Tables["Sales"].Columns["IdSeller"]);
            shopDB.Tables["Sales"].Constraints.Add(foreignKeyConstraint);

            ForeignKeyConstraint foreignKeyConstraint1 = new ForeignKeyConstraint("FK_CustomerId", shopDB.Tables["Customers"].Columns["Id"], shopDB.Tables["Sales"].Columns["IdCustomer"]);
            shopDB.Tables["Sales"].Constraints.Add(foreignKeyConstraint1);

            Console.WriteLine("Sales complete");

            DataRow sale1 = sales.NewRow();
            sale1.ItemArray = new object[] { null, 1, 1, DateTime.Now, 100 };
            sales.Rows.Add(sale1);
            sales.Rows.Add(new object[] { null, 2, 2, DateTime.Now, 200 });
            sales.Rows.Add(new object[] { null, 3, 3, DateTime.Now, 300 });
            sales.Rows.Add(new object[] { null, 4, 4, DateTime.Now, 400 });

            Console.Write("\tId \tSellerId \tCustomerId \tDateofSale \tPrice");
            Console.WriteLine();
            foreach (DataRow item in sales.Rows)
            {
                foreach (var cell in item.ItemArray)
                {
                    Console.WriteLine("\t{0}", cell);
                }
                Console.WriteLine();
            }


            //string conn = ConfigurationManager.ConnectionStrings["kuma"].ConnectionString;
            //
            //using (SqlConnection db = new SqlConnection(conn))
            //{
            //    try
            //    {
            //        db.Open();
            //
            //        
            //    }
            //    catch (SqlException ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //        throw;
            //    }
            //    finally
            //    {
            //        Console.WriteLine("Create complete");
            //        db.Close();
            //        Console.ReadKey();
            //    }
            //}
        }
    }
}
