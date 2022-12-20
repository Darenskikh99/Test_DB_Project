using System;
using System.Data.SqlClient;

namespace Test.Database
{
    public class DatabaseManager
    {
        private readonly SqlConnection _sqlConnection;

        public DatabaseManager(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException("Не заданы параметры подключения к базе данных");
            }

            _sqlConnection = new SqlConnection(connectionString);
        }

        public SqlConnection OpenConection()
        {
            if (_sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                _sqlConnection.Open();
            }

            return _sqlConnection;
        }

        public void CloseConection()
        {
            if (_sqlConnection.State == System.Data.ConnectionState.Open)
            {
                _sqlConnection.Close();
            }
        }
    }
}