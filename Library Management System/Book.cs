using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Library_Management_System
{
    internal class Book
    {
        const string ConnectionString = "Server=.;Database=LibraryManagementSystem;User Id=sa;Password=sa123456;TrustServerCertificate=True;";

        public int BookID { get; set; }  
        public string Title { get; set; }
        public string Author { get; set; }
        public int AvailableCopies { get; set; }  
        public int TotalCopies { get; set; } 
        public string Category { get; set; }

        // Add a New Book
        public void AddBook()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Books (Title, Author, Category, AvailableCopies, TotalCopies) VALUES (@Title, @Author, @Category, @AvailableCopies, @TotalCopies);";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Title", Title);
                        cmd.Parameters.AddWithValue("@Author", Author);
                        cmd.Parameters.AddWithValue("@Category", Category);
                        cmd.Parameters.AddWithValue("@AvailableCopies", AvailableCopies);
                        cmd.Parameters.AddWithValue("@TotalCopies", TotalCopies);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0) Console.WriteLine("Book added successfully!");
                        else Console.WriteLine("Failed to add the book");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        // Remove a Book
        public void RemoveBook()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM Books WHERE ID = @BookID;";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@BookID", BookID);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0) Console.WriteLine(" Book removed successfully!");
                        else Console.WriteLine(" Book not found");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        //  Update Book Information
        public void UpdateBook()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE Books SET Title = @Title, Author = @Author, Category = @Category WHERE ID = @BookID;";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@BookID", BookID);
                        cmd.Parameters.AddWithValue("@Title", Title);
                        cmd.Parameters.AddWithValue("@Author", Author);
                        cmd.Parameters.AddWithValue("@Category", Category);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0) Console.WriteLine(" Book updated successfully!");
                        else Console.WriteLine(" Book not found");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        //  Borrow a Book
        public void BorrowBook()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE Books SET AvailableCopies = AvailableCopies - 1 WHERE ID = @BookID AND AvailableCopies > 0;";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@BookID", BookID);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0) Console.WriteLine("📖 Book borrowed successfully!");
                        else Console.WriteLine("❌ Book is not available");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        // Return a Book
        public void ReturnBook()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE Books SET AvailableCopies = AvailableCopies + 1 WHERE ID = @BookID;";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@BookID", BookID);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0) Console.WriteLine(" Book returned successfully!");
                        else Console.WriteLine(" Book return failed");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        // Check if a Book is Available
        public bool CheckAvailability()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT AvailableCopies FROM Books WHERE ID = @BookID;";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@BookID", BookID);

                        object result = cmd.ExecuteScalar(); 
                        if (result != null && Convert.ToInt32(result) > 0)
                        {
                            Console.WriteLine(" Book is available");
                            return true;
                        }
                        else
                        {
                            Console.WriteLine(" Book is not available");
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    return false;
                }
            }
        }

        //  Search for a Book
        public void SearchForBook(string Title,string Author)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM Books WHERE Title = @Title or Author = @Author;";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Title",Title);
                        cmd.Parameters.AddWithValue("@Author", Author);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine($" {reader["Title"]} by {reader["Author"]} - Category: {reader["Category"]}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        // List All Books
        public void ListAllBooks()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM Books;";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine($" {reader["Title"]} by {reader["Author"]} - {reader["AvailableCopies"]} available");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }
}
