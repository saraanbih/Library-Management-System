using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Library_Management_System
{
    internal class User
    {
        const string ConnectionString = "Server=.;Database=LibraryManagementSystem;User Id=sa;Password=sa123456;TrustServerCertificate=True;";

        public int UserID { get;  set; }
        public string Name { get; set; }
        public int RoleID { get; set; } // 1 = Librarian, 2 = Member
        public string CardNumber { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        //  Add a New User (Only Librarians)
        public void AddUser(User currentUser, User newUser)
        {
            if (currentUser.RoleID != 1)
            {
                Console.WriteLine("Access Denied! Only librarians can add users");
                return;
            }

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Users (CardNumber, Name, Phone, Email, RoleID) VALUES (@CardNumber, @Name, @Phone, @Email, @RoleID);";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CardNumber", newUser.CardNumber);
                        cmd.Parameters.AddWithValue("@Name", newUser.Name);
                        cmd.Parameters.AddWithValue("@Phone", newUser.Phone);
                        cmd.Parameters.AddWithValue("@Email", newUser.Email);
                        cmd.Parameters.AddWithValue("@RoleID", newUser.RoleID);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        Console.WriteLine(rowsAffected > 0 ? "User added successfully!" : "Failed to add user");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        //  Remove a User (Only Librarians)
        public void RemoveUser(User currentUser, int userID)
        {
            if (currentUser.RoleID != 1)
            {
                Console.WriteLine("Access Denied! Only librarians can remove users");
                return;
            }

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM Users WHERE UserID = @UserID;";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userID);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        Console.WriteLine(rowsAffected > 0 ? "User removed successfully!" : "User not found");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        //  Update User Information (Only Librarians)
        public void UpdateUser(User currentUser, User updatedUser)
        {
            if (currentUser.RoleID != 1)
            {
                Console.WriteLine("Access Denied! Only librarians can update users.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE Users SET Name = @Name, Phone = @Phone, Email = @Email, RoleID = @RoleID WHERE UserID = @UserID;";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", updatedUser.UserID);
                        cmd.Parameters.AddWithValue("@Name", updatedUser.Name);
                        cmd.Parameters.AddWithValue("@Phone", updatedUser.Phone);
                        cmd.Parameters.AddWithValue("@Email", updatedUser.Email);
                        cmd.Parameters.AddWithValue("@RoleID", updatedUser.RoleID);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        Console.WriteLine(rowsAffected > 0 ? "User updated successfully!" : "User update failed");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        //  Search for a User
        public User SearchUser(string cardNumber)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM Users WHERE CardNumber = @CardNumber;";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CardNumber", cardNumber);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new User
                                {
                                    UserID = reader.GetInt32(0),
                                    CardNumber = reader.GetString(1),
                                    Name = reader.GetString(2),
                                    Phone = reader.GetString(3),
                                    Email = reader.GetString(4),
                                    RoleID = reader.GetInt32(5)
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            return null;
        }

        //  List All Users
        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM Users;";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                users.Add(new User
                                {
                                    UserID = reader.GetInt32(0),
                                    CardNumber = reader.GetString(1),
                                    Name = reader.GetString(2),
                                    Phone = reader.GetString(3),
                                    Email = reader.GetString(4),
                                    RoleID = reader.GetInt32(5)
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            return users;
        }

        //  Borrow a Book (Only Members)
        public void BorrowBook(User user, int bookID)
        {
            if (user.RoleID != 2)
            {
                Console.WriteLine("Access Denied! Only members can borrow books");
                return;
            }

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE Books SET AvailableCopies = AvailableCopies - 1 WHERE BookID = @BookID AND AvailableCopies > 0;";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@BookID", bookID);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        Console.WriteLine(rowsAffected > 0 ? "Book borrowed successfully!" : "Book not available");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        //  Display User Role
        public string GetRoleName(int roleID)
        {
            return roleID == 1 ? "Librarian" : "Member";
        }
    }
}
