using System.Data.SqlClient;

namespace GenFormDBDll
{
    class DBConnectMsSql
    {


        public SqlConnection connection = new SqlConnection(connectionString);
        public static string connectionString { get; set; }

        public void sqlconnection()
        {
            connection = new SqlConnection(connectionString);

        }
    }
}
