using System;
using System.Data.SQLite;

namespace SQLiteExample
{
    public class ViewTask
    {
        private readonly string _dbFile;

        public ViewTask(string dbFile)
        {
            _dbFile = dbFile;
        }

        public void ViewTasks()
        {
            using var connection = new SQLiteConnection($"Data Source={_dbFile};Version=3;");
            connection.Open();

            string query = "SELECT * FROM Tasks ORDER BY CreatedAt DESC";
            using var command = new SQLiteCommand(query, connection);
            using var reader = command.ExecuteReader();

            Console.WriteLine("\n--- Task List ---");
            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader["Id"]}");
                Console.WriteLine($"Title: {reader["Title"]}");
                Console.WriteLine($"Description: {reader["Description"]}");
                Console.WriteLine($"Created At: {reader["CreatedAt"]}");
                Console.WriteLine("---------------------------");
            }

            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
        }
    }
}

