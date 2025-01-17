using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public static class SqlHelper
    {
        public static string connectionString { get; } = "Data Source=NTB9\\MSSQLSERVER02; Initial Catalog=contact; Integrated Security=True";
        private static SqlConnection _connection;

        public static SqlConnection OpenConnection(string connectionString)
        {
            if (_connection == null || _connection.State == System.Data.ConnectionState.Closed)
            {
                _connection = new SqlConnection(connectionString);
                _connection.Open();
            }
            return _connection;
        }

        public static void CloseConnection()
        {
            if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
            {
                _connection.Close();
            }
        }

        public static int ExecuteNonQuery(string connectionString, string query, Action<SqlCommand> parameterize)
        {
            using (var cmd = new SqlCommand(query, OpenConnection(connectionString)))
            {
                parameterize(cmd);
                return cmd.ExecuteNonQuery();
            }
        }

        public static T ExecuteScalar<T>(string connectionString, string query, Action<SqlCommand> parameterize)
        {
            using (var cmd = new SqlCommand(query, OpenConnection(connectionString)))
            {
                parameterize(cmd);
                return (T)cmd.ExecuteScalar();
            }
        }

        public static void ExecuteReader(string connectionString, string query, Action<SqlCommand> parameterize, Action<SqlDataReader> handleData)
        {
            using (var cmd = new SqlCommand(query, OpenConnection(connectionString)))
            {
                parameterize(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    handleData(reader);
                }
            }
        }
    }
}

