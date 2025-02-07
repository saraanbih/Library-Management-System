using Library_Management_System;
using System;
using System.Collections.Generic;

namespace LibraryManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Library Management System ===");
                Console.WriteLine("1. Manage Books");
                Console.WriteLine("2. Manage Users");
                Console.WriteLine("3. Exit");
                Console.Write("Enter your choice: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        ManageBooks();
                        break;
                    case "2":
                        ManageUsers();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Press any key to try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void ManageBooks()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Book Management ===");
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. Remove Book");
                Console.WriteLine("3. Update Book");
                Console.WriteLine("4. Borrow Book");
                Console.WriteLine("5. Return Book");
                Console.WriteLine("6. List All Books");
                Console.WriteLine("7. Back to Main Menu");
                Console.Write("Enter your choice: ");

                Book book = new Book();
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Write("Enter title: ");
                        book.Title = Console.ReadLine();
                        Console.Write("Enter author: ");
                        book.Author = Console.ReadLine();
                        Console.Write("Enter category: ");
                        book.Category = Console.ReadLine();
                        Console.Write("Enter total copies: ");
                        book.TotalCopies = int.Parse(Console.ReadLine());
                        book.AvailableCopies = book.TotalCopies;
                        book.AddBook();
                        break;

                    case "2":
                        Console.Write("Enter BookID to remove: ");
                        book.BookID = int.Parse(Console.ReadLine());
                        book.RemoveBook();
                        break;

                    case "3":
                        Console.Write("Enter BookID to update: ");
                        book.BookID = int.Parse(Console.ReadLine());
                        Console.Write("Enter new title: ");
                        book.Title = Console.ReadLine();
                        Console.Write("Enter new author: ");
                        book.Author = Console.ReadLine();
                        Console.Write("Enter new category: ");
                        book.Category = Console.ReadLine();
                        book.UpdateBook();
                        break;

                    case "4":
                        Console.Write("Enter BookID to borrow: ");
                        book.BookID = int.Parse(Console.ReadLine());
                        book.BorrowBook();
                        break;

                    case "5":
                        Console.Write("Enter BookID to return: ");
                        book.BookID = int.Parse(Console.ReadLine());
                        book.ReturnBook();
                        break;

                    case "6":
                        book.ListAllBooks();
                        break;

                    case "7":
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Press any key to try again");
                        Console.ReadKey();
                        break;
                }
                Console.ReadKey();
            }
        }

        static void ManageUsers()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== User Management ===");
                Console.WriteLine("1. Add User");
                Console.WriteLine("2. Remove User");
                Console.WriteLine("3. Update User");
                Console.WriteLine("4. List All Users");
                Console.WriteLine("5. Back to Main Menu");
                Console.Write("Enter your choice: ");

                User user = new User();
                User librarian = new User { UserID = 1, RoleID = 1 }; // Assuming librarian with UserID = 1

                switch (Console.ReadLine())
                {
                    case "1":
                        User newUser = new User();
                        Console.Write("Enter card number: ");
                        newUser.CardNumber = Console.ReadLine();
                        Console.Write("Enter name: ");
                        newUser.Name = Console.ReadLine();
                        Console.Write("Enter phone: ");
                        newUser.Phone = Console.ReadLine();
                        Console.Write("Enter email: ");
                        newUser.Email = Console.ReadLine();
                        Console.Write("Enter role ID (1 for Librarian, 2 for Member): ");
                        newUser.RoleID = int.Parse(Console.ReadLine());
                        librarian.AddUser(librarian, newUser);
                        break;

                    case "2":
                        Console.Write("Enter UserID to remove: ");
                        int removeUserId = int.Parse(Console.ReadLine());
                        librarian.RemoveUser(librarian, removeUserId);
                        break;

                    case "3":
                        User updatedUser = new User();
                        Console.Write("Enter UserID to update: ");
                        updatedUser.UserID = int.Parse(Console.ReadLine());
                        Console.Write("Enter new name: ");
                        updatedUser.Name = Console.ReadLine();
                        Console.Write("Enter new phone: ");
                        updatedUser.Phone = Console.ReadLine();
                        Console.Write("Enter new email: ");
                        updatedUser.Email = Console.ReadLine();
                        Console.Write("Enter new role ID (1 for Librarian, 2 for Member): ");
                        updatedUser.RoleID = int.Parse(Console.ReadLine());
                        librarian.UpdateUser(librarian, updatedUser);
                        break;

                    case "4":
                        List<User> users = user.GetAllUsers();
                        foreach (var u in users)
                        {
                            Console.WriteLine($"{u.Name} - {u.GetRoleName(u.RoleID)}");
                        }
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;

                    case "5":
                        return;

                    default:
                        Console.WriteLine("Invalid choice Press any key to try again");
                        Console.ReadKey();
                        break;
                }
            }  
        }
    }
}
