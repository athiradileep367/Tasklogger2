using System;

namespace SQLiteExample
{
    class Program
    {
        static void Main(string[] args)
        {
            string dbFile = "example.db";

            var initializer = new DBInitializer();
            initializer.Initialize(dbFile);

            var adder = new AddTaskH(dbFile);
            var updater = new UpdateTaskh(dbFile);
            var deleter = new DeleteTaskh(dbFile);
            var viewer = new ViewTask(dbFile);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== Task Manager ====");
                Console.WriteLine("1. Add Task");
                Console.WriteLine("2. View Tasks");
                Console.WriteLine("3. Delete Task");
                Console.WriteLine("4. Update Task");
                Console.WriteLine("5. Exit");
                Console.Write("Choose an option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": adder.AddTask(); break;
                    case "2": viewer.ViewTasks(); break;
                    case "3": deleter.DeleteTask(); break;
                    case "4": updater.UpdateTask(); break;
                    case "5": return;
                    default:
                        Console.WriteLine("Invalid choice. Press Enter.");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }
}
