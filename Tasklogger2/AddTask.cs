using System;
using System.Data.SQLite;

namespace SQLiteExample
{
    public class AddTaskH
    {
        private readonly string _dbFile;

        public AddTaskH(string dbFile)
        {
            _dbFile = dbFile;
        }

        public void AddTask()
        {
            Console.Write("Enter task title: ");
            string title = Console.ReadLine();

            Console.Write("Enter task description: ");
            string description = Console.ReadLine();

            using var connection = new SQLiteConnection($"Data Source={_dbFile};Version=3;");
            connection.Open();

            string query = "INSERT INTO Tasks (Title, Description) VALUES (@title, @description)";
            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@title", title);
            command.Parameters.AddWithValue("@description", description);
            command.ExecuteNonQuery();

            Console.WriteLine("Task added. Press Enter to continue.");
            Console.ReadLine();
        }
    }
}

