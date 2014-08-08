using System;
using System.Data;
using System.Data.SqlClient;

namespace LateComerTracker.DataAccess
{
    public class DataAccessObject
    {
        private readonly string _connectionString;

        public DataAccessObject(string connString)
        {
            _connectionString = connString;
        }

        public DataTable GetList(string commandText)
        {
            var table = new DataTable();
            using (var connection = new SqlConnection(_connectionString))
            {
                var adapter = new SqlDataAdapter(commandText, connection);
                adapter.Fill(table);
            }
            return table;
        }

        public int ExecuteNonQuery(string commandText)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = commandText;

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    return 1;
                }
                catch (Exception exc)
                {
                    //log exc
                    return -1;
                }
            }
        }

        public object ExecuteScalar(string commandText)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(commandText, connection))
                {
                    return command.ExecuteScalar();
                }
            }
        }
    }
}
