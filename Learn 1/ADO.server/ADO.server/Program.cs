using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration; // for ConfigurationManager

namespace ADO.server
{
    internal class Program
    {

        public static string connStr;
        static void Main(string[] args)
        {
            connStr = ConfigurationManager.ConnectionStrings["kuma"].ConnectionString;
        }
    }
}
