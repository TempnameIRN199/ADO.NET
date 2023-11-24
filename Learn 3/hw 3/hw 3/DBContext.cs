using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace hw_3
{
    internal class DBContext
    {
        public SqlConnection conn = null;
        public DBContext()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["MyConn"].ConnectionString;
            conn = new SqlConnection(ConnectionString);
        }

    }
}
