using Apps72.Dev.Data;
using System;
using System.Data.SqlClient;
using System.IO;

namespace Core.Tests.Data
{
    public class LocalDbBuilder
    {
        private readonly string masterConnectionString;
        private readonly string dbName;
        public string ConnectionString { get; }

        public LocalDbBuilder(string dbName)
        {
            this.dbName = dbName;
            masterConnectionString = @"data source=(localdb)\MSSQLLocalDB;";
            ConnectionString = $@"{masterConnectionString}Database={dbName}";
        }

        public LocalDbBuilder Create()
        {
            var script = $"CREATE DATABASE {dbName}";
            using (var connection = new SqlConnection(masterConnectionString))
            using (var databaseCommand = new DatabaseCommand(connection))
            {
                connection.Open();
                databaseCommand.CommandText.AppendLine(script);
                databaseCommand.ExecuteNonQuery();
            }
            return this;
        }

        public LocalDbBuilder Drop()
        {
            var script = $@"
                ALTER DATABASE {dbName} SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                DROP DATABASE {dbName};";

            using (var connection = new SqlConnection(masterConnectionString))
            using (var databaseCommand = new DatabaseCommand(connection))
            {
                connection.Open();
                databaseCommand.CommandText.AppendLine(script);
                databaseCommand.ExecuteNonQuery();
            }
            return this;
        }

        public LocalDbBuilder ExecuteFile(string path)
        {
            var sqlScript = File.ReadAllText(path);
            return ExecuteScript(sqlScript);
        }

        public LocalDbBuilder ExecuteScript(string sqlScript)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                using (var databaseCommand = new DatabaseCommand(connection))
                {
                    connection.Open();
                    databaseCommand.CommandText.AppendLine(sqlScript);
                    databaseCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return this;
        }
    }
}
