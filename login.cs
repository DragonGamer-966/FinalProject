
using System;
using System.IO;

namespace BookManagement
{
    public class login
    {
        private string filePath;

        public login(string path)
        {
            filePath = path;
        }

        public bool LoginUser()
        {
            Console.WriteLine("\n===== LOGIN =====");

            Console.Write("Username: ");
            string username = (Console.ReadLine() ?? "").Trim();

            Console.Write("Password: ");
            string password = Console.ReadLine() ?? "";

            if (!File.Exists(filePath))
            {
                Console.WriteLine("User file not found!");
                return false;
            }

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] parts = line.Split('|');

                if (parts.Length == 2 &&
                    parts[0] == username &&
                    parts[1] == password)
                {
                    Console.WriteLine("\nLogin successful!");
                    Console.WriteLine($"Welcome, {username}!");
                    return true;
                }
            }

            Console.WriteLine("\nWrong username or password!");
            return false;
        }
    }
}
