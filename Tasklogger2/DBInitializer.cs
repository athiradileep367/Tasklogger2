using System;
using System.Data.SQLite;
using System.IO;

namespace SQLiteExample
{
    public class DBInitializer
    {
        public void Initialize(string dbFile)
        {
            try
            {
                if (!File.Exists(dbFile))
                {
                    SQLiteConnection.CreateFile(dbFile);
                    Console.WriteLine("Database file created.");
                }

                using var connection = new SQLiteConnection($"Data Source={dbFile};Version=3;");
                connection.Open();

                string createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS Tasks (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Title TEXT NOT NULL,
                        Description TEXT,
                        CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
                    );";

                using var command = new SQLiteCommand(createTableQuery, connection);
                command.ExecuteNonQuery();
                Console.WriteLine("Table ready.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error initializing database: " + ex.Message);
            }
        }
    }
}

