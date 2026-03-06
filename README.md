# Library Management System

A feature-rich **Library Management System** built in C# to manage books, users, and library operations efficiently

---

## Features

### Book Management
- Add new books with title, author, category, and availability details
- Update book information easily
- Delete books from the system
- Search books by title or author
- List all available books
- Borrow and return books with availability tracking

### User Management
- Add new users (restricted to librarians)
- Update user details (restricted to librarians)
- Remove users (restricted to librarians)
- Search users by card number
- List all registered users

### Role-Based Access
- **Librarian**: Full access to all features, including managing users and books
- **Member**: Limited access for borrowing and returning books

---

## Getting Started

### Prerequisites
1. **SQL Server**: Ensure SQL Server is installed and running
2. **.NET SDK**: Download and install the [.NET SDK](https://dotnet.microsoft.com/download)

---

### Database Setup

1. Create a database named `LibraryManagementSystem` in SQL Server.
2. Use the following SQL scripts to set up the required tables:

#### Books Table
```sql
CREATE TABLE Books (
    BookID INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(255) NOT NULL,
    Author NVARCHAR(255) NOT NULL,
    Category NVARCHAR(100),
    AvailableCopies INT NOT NULL,
    TotalCopies INT NOT NULL
);
```

#### Users Table
```sql
CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    Role NVARCHAR(50) NOT NULL,
    CardNumber NVARCHAR(50) UNIQUE NOT NULL
);
```

---

## How to Run the Project

1. Clone this repository:
   ```bash
   git clone https://github.com/saraanbih/library-management-system.git
   ```
2. Open the project in Visual Studio.
3. Configure the database connection string in `appsettings.json`
4. Build and run the application:
   ```bash
   dotnet run
   ```

---

## Role Definitions

| Role       | Permissions                                         |
|------------|-----------------------------------------------------|
| Librarian  | Manage books and users, borrow and return books     |
| Member     | Borrow and return books only                        |

---

## Future Enhancements
- Add book borrowing history
- Implement a user-friendly GUI
- Generate reports for borrowed and available books
- Add authentication for secure access

---

## Contributing
Contributions are welcome! Feel free to submit a pull request

---

## Author
**Sara Nabih**  
[Email](nabihsara8@gmail.com)

🐦 [Telegram](https://t.me/Sara_Nabih)
