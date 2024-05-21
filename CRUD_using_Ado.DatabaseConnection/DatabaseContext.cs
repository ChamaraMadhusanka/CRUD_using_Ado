using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_using_Ado.DatabaseConnection
{
    public class DatabaseContext
    {
        private readonly string connectionString = "Data Source=VIMUKTHI;Initial Catalog=DB_Fro_Ado.Net;Integrated Security=True";
        //private readonly string connectionString = "Data Source=VIMUKTHI;Initial Catalog=Sample_DB;Integrated Security=True";
        private SqlConnection connection = null;

        public SqlConnection createConnection()
        {
            try
            {
                if (connection == null)
                {
                    connection = new SqlConnection(connectionString);
                    return connection;
                }
                else
                {
                    Console.WriteLine("Connection is already open.");
                    return connection;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error opening connection: " + ex.Message);
                return connection;
            }
        }
    }
}
