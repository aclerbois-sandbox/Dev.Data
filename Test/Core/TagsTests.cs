using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Apps72.Dev.Data;
using System.Data.SqlClient;
using System.Linq;
using Core.Tests.Data;

namespace Data.Core.Tests
{
    /*
        To run these tests, you must have the SCOTT database (scott.sql)
        and you need to configure your connection string (configuration.cs)
    */

    [TestClass]
    public class TagsTests
    {
        private static readonly string NEW_LINE = Environment.NewLine;

        #region INITIALIZATION

        private SqlConnection _connection;
        private LocalDbBuilder _database;

        [TestInitialize]
        public void Initialization()
        {
            var dbName = $"Apps72_{DateTime.Now:yyyyMMdd_HHmmssfff}";
            _database = new LocalDbBuilder(dbName);
            _database
                .Create()
                .ExecuteFile(@"Data/Scott.sql");


            _connection = new SqlConnection(_database.ConnectionString);
            _connection.Open();
        }

        [TestCleanup]
        public void Finalization()
        {
            if (_connection != null)
            {
                _connection.Close();
                _connection.Dispose();
                _database?.Drop();
            }
        }


        #endregion

        [TestMethod]
        public void SimpleTag_Test()
        {
            using (var cmd = new DatabaseCommand(_connection))
            {
                cmd.Log = Console.WriteLine;
                cmd.TagWith("My command");
                cmd.CommandText = "SELECT * FROM EMP";

                cmd.ActionBeforeExecution = (query) =>
                {
                    Assert.AreEqual($"My command", query.Tags.First());
                    Assert.AreEqual($"SELECT * FROM EMP", query.CommandText);
                    Assert.AreEqual($"-- My command{NEW_LINE}SELECT * FROM EMP", query.Formatted.CommandAsText);
                };

                cmd.ExecuteNonQuery();
            }
        }

        [TestMethod]
        public void MultipleTags_Test()
        {
            using (var cmd = new DatabaseCommand(_connection))
            {
                cmd.Log = Console.WriteLine;
                cmd.TagWith("Tag1");
                cmd.TagWith("Tag2");
                cmd.CommandText = "SELECT * FROM EMP";

                cmd.ActionBeforeExecution = (query) =>
                {
                    Assert.AreEqual($"Tag1", query.Tags.ElementAt(0));
                    Assert.AreEqual($"Tag2", query.Tags.ElementAt(1));
                    Assert.AreEqual($"SELECT * FROM EMP", query.CommandText);
                    Assert.AreEqual($"-- Tag1{NEW_LINE}-- Tag2{NEW_LINE}SELECT * FROM EMP", query.Formatted.CommandAsText);
                };

                cmd.ExecuteNonQuery();
            }
        }

        [TestMethod]
        public void TagMultilineTags_Test()
        {
            using (var cmd = new DatabaseCommand(_connection))
            {
                cmd.Log = Console.WriteLine;
                cmd.TagWith($"Tag1{NEW_LINE}Tag2");
                cmd.CommandText = "SELECT * FROM EMP";

                cmd.ActionBeforeExecution = (query) =>
                {
                    Assert.AreEqual($"Tag1", query.Tags.ElementAt(0));
                    Assert.AreEqual($"Tag2", query.Tags.ElementAt(1));
                    Assert.AreEqual($"SELECT * FROM EMP", query.CommandText);
                    Assert.AreEqual($"-- Tag1{NEW_LINE}-- Tag2{NEW_LINE}SELECT * FROM EMP", query.Formatted.CommandAsText);
                };

                cmd.ExecuteNonQuery();
            }
        }

        [TestMethod]
        public void UpdateTagAfterExecution_Test()
        {
            using (var cmd = new DatabaseCommand(_connection))
            {
                cmd.Log = Console.WriteLine;

                // Tag 1
                cmd.TagWith("Tag1");
                cmd.CommandText = "SELECT * FROM EMP";

                cmd.ActionBeforeExecution = (query) =>
                {
                    Assert.AreEqual($"Tag1", query.Tags.First());
                    Assert.AreEqual($"SELECT * FROM EMP", query.CommandText);
                    Assert.AreEqual($"-- Tag1{NEW_LINE}SELECT * FROM EMP", query.Formatted.CommandAsText);
                };

                cmd.ExecuteNonQuery();

                // Tag 2
                cmd.TagWith("Tag2");
                cmd.CommandText = "SELECT * FROM EMP";

                cmd.ActionBeforeExecution = (query) =>
                {
                    Assert.AreEqual($"Tag1", query.Tags.First());
                    Assert.AreEqual($"SELECT * FROM EMP", query.CommandText);
                    Assert.AreEqual($"-- Tag1{NEW_LINE}-- Tag2{NEW_LINE}SELECT * FROM EMP", query.Formatted.CommandAsText);
                };

                cmd.ExecuteNonQuery();
            }
        }
    }
}
