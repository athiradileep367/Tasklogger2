using System;
using System.Data.SQLite;
using System.IO;

namespace SQLiteExample
{
    class Program
    {
        static string dbFile = "example.db";

        static void Main(string[] args)
        {
            CreateDatabaseAndTable();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== Task Manager ====");
                Console.WriteLine("1. Add Task");
                Console.WriteLine("2. View Tasks");
                Console.WriteLine("3. Delete Task");
                Console.WriteLine("4. Update Task");
                Console.WriteLine("5. exit");
                Console.Write("Choose an option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddTask();
                        break;
                    case "2":
                        ViewTasks();
                        break;
                    case "3":
                        DeleteTask();
                        break;
                    case "4":
                        UpdateTask();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Press Enter to continue.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        static void CreateDatabaseAndTable()
        {
            if (!File.Exists(dbFile))
            {
                SQLiteConnection.CreateFile(dbFile);
                Console.WriteLine("Database file created.");
            }

            using (var connection = new SQLiteConnection($"Data Source={dbFile};Version=3;"))
            {
                connection.Open();

                string createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS Tasks (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Title TEXT NOT NULL,
                        Description TEXT,
                        CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
                    );";

                using (var command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        static void UpdateTask()
        {
            Console.WriteLine("Enter the Task ID");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID. Press Enter to continue.");
                Console.ReadLine();
                return;
            }
            Console.Write("Enter task details: ");
            string Updatedescription = Console.ReadLine();
            Console.Write("Enter task Description: ");
            string Updatedescriptiondetails = Console.ReadLine();
          

                using (var connection = new SQLiteConnection($"Data Source={dbFile};Version=3;"))
                {
                    connection.Open();

                    string updateQuery = "UPDATE Tasks SET Title= @Updatedescription, Description=@Updatedescriptiondetails WHERE Id=@id ";

                    using (var command = new SQLiteCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@Updatedescription", Updatedescription);
                        command.Parameters.AddWithValue("@Updatedescriptiondetails", Updatedescriptiondetails);
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            Console.WriteLine("Task Updated.");
                        else
                            Console.WriteLine("Task not found.");
                    }

                    Console.WriteLine("Press Enter to continue.");
                    Console.ReadLine();
                }
            }
        

        static void AddTask()
        {
            Console.Write("Enter task title: ");
            string title = Console.ReadLine();

            Console.Write("Enter task description: ");
            string description = Console.ReadLine();

            using (var connection = new SQLiteConnection($"Data Source={dbFile};Version=3;"))
            {
                connection.Open();

                string insertQuery = "INSERT INTO Tasks (Title, Description) VALUES (@title, @description)";

                using (var command = new SQLiteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@title", title);
                    command.Parameters.AddWithValue("@description", description);
                    command.ExecuteNonQuery();
                }

                Console.WriteLine("Task added. Press Enter to continue.");
                Console.ReadLine();
            }
        }

        static void ViewTasks()
        {
            using (var connection = new SQLiteConnection($"Data Source={dbFile};Version=3;"))
            {
                connection.Open();

                string selectQuery = "SELECT * FROM Tasks ORDER BY CreatedAt DESC";

                using (var command = new SQLiteCommand(selectQuery, connection))
                using (var reader = command.ExecuteReader())
                {
                    Console.WriteLine("\n-- Tasks --");
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

        static void DeleteTask()
        {
            Console.Write("Enter Task ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID. Press Enter to continue.");
                Console.ReadLine();
                return;
            }

            using (var connection = new SQLiteConnection($"Data Source={dbFile};Version=3;"))
            {
                connection.Open();

                string deleteQuery = "DELETE FROM Tasks WHERE Id = @id";

                using (var command = new SQLiteCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                        Console.WriteLine("Task deleted.");
                    else
                        Console.WriteLine("Task not found.");
                }

                Console.WriteLine("Press Enter to continue.");
                Console.ReadLine();
            }
        }
    }
}
