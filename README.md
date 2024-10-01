# Library Management System

This project is a simple Library Management System that allows users to manage and borrowing operations.

## Features

-
- **Borrowing Operations:** Track book borrowings, returns, and generate reports.
- **Status Tracking:** Track the status of borrowed books using the `CopyStatus` table.

## Project Structure

The project follows a layered architecture with a clear separation of concerns. It includes:
- **Models:** For managing entities like Books, Copies, Students, and Borrowings.
- **Controllers:** Responsible for handling user requests, including borrowing operations and generating reports.
- **Services:** Business logic for handling operations.
- **Repositories:** Data access layer to interact with the database.

### Database Tables
The system uses the following tables:
1. **Students:** Stores student information.
2. **Books:** Stores book information.
3. **Copies:** Tracks the unique copies of each book.
4. **CopyStatus:** Tracks the status of each book copy (borrowed, available, etc.).
5. **Borrowings:** Logs borrowing operations with borrowing dates.

## Technologies Used

- **ASP.NET Core**
- **Entity Framework Core**
- **MySQL**


