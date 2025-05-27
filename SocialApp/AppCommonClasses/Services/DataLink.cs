using System;
using System.Data;
using System.Data.SqlClient;
using AppCommonClasses.Interfaces;

namespace AppCommonClasses.Services
{
    public class DataLink : IDataLink
    {
        // You may want to inject a connection string or configuration here
        private readonly string _connectionString;

        public DataLink(string connectionString)
        {
            // For demo purposes, set your connection string here or inject it via DI
            _connectionString = connectionString;
        }

        [Obsolete]
        public T? ExecuteScalar<T>(string query, SqlParameter[]? sqlParameters, bool isStoredProcedure)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection)
            {
                CommandType = isStoredProcedure ? CommandType.StoredProcedure : CommandType.Text
            };
            if (sqlParameters != null)
                command.Parameters.AddRange(sqlParameters);

            connection.Open();
            var result = command.ExecuteScalar();
            return result == null || result is DBNull ? default : (T)result;
        }

        [Obsolete]
        public int ExecuteQuery(string query, SqlParameter[]? sqlParameters, bool isStoredProcedure)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection)
            {
                CommandType = isStoredProcedure ? CommandType.StoredProcedure : CommandType.Text
            };
            if (sqlParameters != null)
                command.Parameters.AddRange(sqlParameters);

            connection.Open();
            return command.ExecuteNonQuery();
        }

        [Obsolete]
        public int ExecuteNonQuery(string storedProcedure, SqlParameter[]? sqlParameters)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(storedProcedure, connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            if (sqlParameters != null)
                command.Parameters.AddRange(sqlParameters);

            connection.Open();
            return command.ExecuteNonQuery();
        }

        [Obsolete]
        public DataTable ExecuteSqlQuery(string query, SqlParameter[]? sqlParameters)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);
            if (sqlParameters != null)
                command.Parameters.AddRange(sqlParameters);

            using var adapter = new SqlDataAdapter(command);
            var table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        [Obsolete]
        public DataTable ExecuteReader(string storedProcedure, SqlParameter[]? sqlParameters = null)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(storedProcedure, connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            if (sqlParameters != null)
                command.Parameters.AddRange(sqlParameters);

            connection.Open();
            using var reader = command.ExecuteReader();
            var table = new DataTable();
            table.Load(reader);
            return table;
        }
    }
}
