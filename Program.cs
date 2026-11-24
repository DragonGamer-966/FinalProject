
using Microsoft.Win32;
using System;

namespace BookManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            // უსერის მონაცემებისთვის
            string usersPath = @"C:\Users\iliko\OneDrive\Desktop\Users.txt";

            // წიგნი
            string booksPath = @"C:\Users\iliko\OneDrive\Desktop\Books.txt";

            login login = new login(usersPath);
            register register = new register(usersPath);

            BookManager manager = new BookManager(booksPath);

            bool running = true;

            while (running)
            {
                Console.WriteLine("\n===== LIBRARY SYSTEM =====");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Register");
                Console.WriteLine("3. Exit");

                Console.Write("\nChoose an option: ");
                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":

                        bool loggedIn = login.LoginUser();

                        if (loggedIn)
                        {
                            LibraryMenu(manager);
                        }

                        break;

                    case "2":

                        register.RegisterUser();
                        break;

                    case "3":

                        running = false;
                        Console.WriteLine("Goodbye!");
                        break;

                    default:

                        Console.WriteLine("Invalid choice! Try again.");
                        break;
                }
            }
        }

        static void LibraryMenu(BookManager manager)
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n===== BOOK MANAGEMENT SYSTEM =====");
                Console.WriteLine("1. Add a new book");
                Console.WriteLine("2. Show all books");
                Console.WriteLine("3. Search book by title");
                Console.WriteLine("4. Logout");

                Console.Write("\nChoose an option: ");
                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":

                        Console.Write("Enter book title: ");
                        string title = Console.ReadLine() ?? "";

                        if (string.IsNullOrWhiteSpace(title))
                        {
                            Console.WriteLine("Title cannot be empty!");
                            break;
                        }

                        Console.Write("Enter author: ");
                        string author = Console.ReadLine() ?? "";

                        if (string.IsNullOrWhiteSpace(author))
                        {
                            Console.WriteLine("Author cannot be empty!");
                            break;
                        }

                        Console.Write("Enter publication year: ");
                        string yearInput = Console.ReadLine() ?? "";

                        if (!int.TryParse(yearInput, out int year) ||
                            year < 1 ||
                            year > DateTime.Now.Year)
                        {
                            Console.WriteLine("Invalid publication year!");
                            break;
                        }

                        Book newBook = new Book(
                            title.Trim(),
                            author.Trim(),
                            year
                        );

                        manager.AddBook(newBook);
                        break;

                    case "2":

                        manager.ShowAllBooks();
                        break;

                    case "3":

                        Console.Write("Enter book title: ");
                        string searchTitle = Console.ReadLine() ?? "";

                        if (string.IsNullOrWhiteSpace(searchTitle))
                        {
                            Console.WriteLine("Title cannot be empty!");
                            break;
                        }

                        manager.SearchBook(searchTitle.Trim());
                        break;

                    case "4":

                        running = false;
                        Console.WriteLine("Logged out successfully!");
                        break;

                    default:

                        Console.WriteLine("Invalid choice! Try again.");
                        break;
                }
            }
        }
    }
}
