using System;
using System.Data.SQLite;

namespace SQLiteExample
{
    public class UpdateTaskh
    {
        private readonly string _dbFile;

        public UpdateTaskh(string dbFile)
        {
            _dbFile = dbFile;
        }

        public void UpdateTask()
        {
            Console.Write("Enter Task ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID. Press Enter.");
                Console.ReadLine();
                return;
            }

            Console.Write("Enter new title: ");
            string title = Console.ReadLine();

            Console.Write("Enter new description: ");
            string description = Console.ReadLine();

            using var connection = new SQLiteConnection($"Data Source={_dbFile};Version=3;");
            connection.Open();

            string query = "UPDATE Tasks SET Title = @title, Description = @description WHERE Id = @id";
            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@title", title);
            command.Parameters.AddWithValue("@description", description);

            int rows = command.ExecuteNonQuery();
            Console.WriteLine(rows > 0 ? "Task updated." : "Task not found.");
            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
        }
    }
}

