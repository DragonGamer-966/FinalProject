
using System;
using System.IO;
using System.Collections.Generic;

namespace BookManagement
{
    public class BookManager
    {
        private List<Book> books = new List<Book>();
        private string filePath;

        public BookManager(string path)
        {
            filePath = path;
            LoadBooks();
        }

        private void LoadBooks()
        {
            books.Clear();

            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
                return;
            }

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] parts = line.Split('|');

                if (parts.Length == 3 &&
                    int.TryParse(parts[2], out int year))
                {
                    books.Add(new Book(
                        parts[0],
                        parts[1],
                        year
                    ));
                }
            }
        }

        public void AddBook(Book book)
        {
            if (book.Title.Contains("|") || book.Author.Contains("|"))
            {
                Console.WriteLine("Invalid characters in book information!");
                return;
            }

            books.Add(book);

            string line = $"{book.Title}|{book.Author}|{book.Year}";

            File.AppendAllText(filePath, line + Environment.NewLine);

            Console.WriteLine("Book added successfully!");
        }

        public void ShowAllBooks()
        {
            if (books.Count == 0)
            {
                Console.WriteLine("No books available.");
                return;
            }

            Console.WriteLine("\n===== BOOK LIST =====");

            foreach (Book book in books)
            {
                book.DisplayInfo();
            }
        }

        public void SearchBook(string title)
        {
            Book foundBook = books.Find(
                b => string.Equals(
                    b.Title,
                    title,
                    StringComparison.OrdinalIgnoreCase
                )
            );

            if (foundBook != null)
            {
                Console.WriteLine("\nBook found:");
                foundBook.DisplayInfo();
            }
            else
            {
                Console.WriteLine("Book not found!");
            }
        }
    }
}
