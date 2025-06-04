using System;
using System.Data.SQLite;

namespace SQLiteExample
{
    public class DeleteTaskh
    {
        private readonly string _dbFile;

        public DeleteTaskh(string dbFile)
        {
            _dbFile = dbFile;
        }

        public void DeleteTask()
        {
            Console.Write("Enter Task ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID. Press Enter.");
                Console.ReadLine();
                return;
            }

            using var connection = new SQLiteConnection($"Data Source={_dbFile};Version=3;");
            connection.Open();

            string query = "DELETE FROM Tasks WHERE Id = @id";
            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            int rows = command.ExecuteNonQuery();

            Console.WriteLine(rows > 0 ? "Task deleted." : "Task not found.");
            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
        }
    }
}
