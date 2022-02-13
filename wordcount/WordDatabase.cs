using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace Indexer
{
    public class WordDatabase
    {
        private SqliteConnection _connection;
        public WordDatabase()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();

            connectionStringBuilder.DataSource = "./wordCountDB.db";


            _connection = new SqliteConnection(connectionStringBuilder.ConnectionString);

            _connection.Open();

            //Create a table (drop if already exists first):

            var delTableCmd = _connection.CreateCommand();
            delTableCmd.CommandText = "DROP TABLE IF EXISTS word";
            delTableCmd.ExecuteNonQuery();

            var createTableCmd = _connection.CreateCommand();
            createTableCmd.CommandText = "CREATE TABLE word(id INTEGER PRIMARY KEY AUTOINCREMENT, name VARCHAR(50), count INTEGER)";
            createTableCmd.ExecuteNonQuery();

        }

        internal void insertAll(Dictionary<string, int> res)
        {
            using (var transaction = _connection.BeginTransaction())
            {
                var command = _connection.CreateCommand();
                command.CommandText =
                @"INSERT INTO word(name, count) VALUES(@name,@count)";

                var paramName = command.CreateParameter();
                paramName.ParameterName = "name";
                command.Parameters.Add(paramName);

                var paramCount = command.CreateParameter();
                paramCount.ParameterName = "count";
                command.Parameters.Add(paramCount);

                // Insert all entries in the res
                
                foreach (var p in res)
                {
                    paramName.Value = p.Key;
                    paramCount.Value = p.Value;
                    command.ExecuteNonQuery();
                }

                transaction.Commit();
            }
        }

        public void Insert(string word, int count)
        {

            var insertCmd = new SqliteCommand("INSERT INTO word(name, count) VALUES(@name,@count)");
            insertCmd.Connection = _connection;

            var pName = new SqliteParameter("name", word);
            insertCmd.Parameters.Add(pName);

            var pCount = new SqliteParameter("count", count);
            insertCmd.Parameters.Add(pCount);

            insertCmd.ExecuteNonQuery();

        }

        public Dictionary<string, int> GetAll()
        {
            Dictionary<string, int> res = new Dictionary<string, int>();
      
            var selectCmd = _connection.CreateCommand();
            selectCmd.CommandText = "SELECT * FROM word order by count desc";

            using (var reader = selectCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var id = reader.GetInt32(0);
                    var w = reader.GetString(1);
                    var c = reader.GetInt32(2);
                    res.Add(w, c);
                }
            }
            return res;
        }
    }
}
