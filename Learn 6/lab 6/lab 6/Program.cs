using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace lab_6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            SqlConnection connection = null;
            builder.DataSource = @"DESKTOP-RACOKG7";
            builder.InitialCatalog = "ShopDB";
            builder.IntegratedSecurity = true;
            connection = new SqlConnection(builder.ConnectionString);
            connection.Open();

            SqlCommand command = connection.CreateCommand();

            command.CommandText = "create table Sales (SaleID int not null identity primary key,CustomerID int, SellerID int, Amount decimal)";
            command.ExecuteNonQuery();

            command.CommandText = "create table Customers(CustomerID int not null identity primary key, [Name] nvarchar(100), Email nvarchar(100))";
            command.ExecuteNonQuery();

            command.CommandText = "create table Sellers(SellerID int not null identity primary key,[Name] nvarchar(100),[Address] nvarchar(100))";
            command.ExecuteNonQuery();

            command.CommandText = "ALTER TABLE Sales ADD CONSTRAINT FK_Sales_Customers FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)";
            command.ExecuteNonQuery();

            command.CommandText = "ALTER TABLE Sales ADD CONSTRAINT FK_Sales_Sellers FOREIGN KEY (SellerID) REFERENCES Sellers(SellerID)";
            command.ExecuteNonQuery();

            connection.Close();

            Console.WriteLine("Таблицы созданы и ограничения добавлены в базу данных ShopDB.");
        }
    }
}
