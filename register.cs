
using System;
using System.IO;

namespace BookManagement
{
    public class register
    {
        private string filePath;

        public register(string path)
        {
            filePath = path;
        }

        public bool RegisterUser()
        {
            Console.WriteLine("\n===== REGISTER =====");

            Console.Write("Choose username: ");
            string username = (Console.ReadLine() ?? "").Trim();

            Console.Write("Choose password: ");
            string password = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("Username and password cannot be empty!");
                return false;
            }

            if (username.Contains("|") || password.Contains("|"))
            {
                Console.WriteLine("The | character is not allowed!");
                return false;
            }

            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] parts = line.Split('|');

                if (parts.Length == 2 &&
                    parts[0].Equals(
                        username,
                        StringComparison.OrdinalIgnoreCase
                    ))
                {
                    Console.WriteLine("Username already exists!");
                    return false;
                }
            }

            string newUser = $"{username}|{password}";

            File.AppendAllText(
                filePath,
                newUser + Environment.NewLine
            );

            Console.WriteLine("\nRegistration successful!");
            return true;
        }
    }
}
