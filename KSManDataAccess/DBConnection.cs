using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSManDataAccess
{
    class DBConnection
    {
        internal static SqlConnection GetConnection()
        {
            // Connection to central database
            var connString = @"Data Source=DESKTOP-O5U6AVG\SQLEXPRESS;Initial Catalog=ksmanDB;Integrated Security=True";
            var conn = new SqlConnection(connString);
            return conn;
        }
    }
}
