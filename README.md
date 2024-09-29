# The Book Haven - Library Management System (ASP.NET Core MVC)

## Overview

**The Book Haven** is a Library Management System built using **ASP.NET Core MVC** and **Entity Framework Core**. It allows users to manage books, track borrow/return operations, and calculate penalties for overdue books. The system also includes member management and a search feature for books.

## Features

- **Book Borrowing**: Allows users to borrow books from the library with a predefined loan period.
- **Book Returning**: Users can return borrowed books, with penalties applied for overdue returns.
- **Penalty Calculation**: Calculates and tracks penalties for books returned after the due date.
- **Book Availability**: Updates and tracks the availability of books based on the number of copies available.
- **Member Management**: Handles basic member information such as adding and viewing members.
- **Book Search**: Provides a search functionality for finding available books by title.

## Technologies Used

- **ASP.NET Core MVC**: For building the web application and handling the user interface.
- **Entity Framework Core**: As the ORM (Object-Relational Mapping) tool to manage the database.
- **SQL Server**: The relational database management system for storing library data.
- **Bootstrap**: For responsive and modern UI components.

## Project Structure

This project follows the **MVC** (Model-View-Controller) pattern:

- **Models**: Represent the business entities such as `Book`, `Member`, `Loan`, and `Penalty`.
- **Views**: Handle the user interface for borrowing, returning, and viewing books.
- **Controllers**: Handle the interaction between models and views, performing actions like borrowing and returning books.

## Entity-Relationship Diagram (ERD)

- **Books**: Store book details such as title, author, and available copies.
- **Members**: Store information about library members.
- **Loans**: Track the borrowing and returning of books, along with loan dates and due dates.
- **Penalties**: Track penalties for books returned after the due date.

## Installation Instructions

### 1. Clone the repository

```bash
    git clone https://github.com/yourusername/the-book-haven.git
```
### 2. Navigate to the project directory
```bash
    cd the-book-haven
```
### 3. Set up the database
Update the connection string in appsettings.json to match your SQL Server instance.
Run the following command to create the database and apply migrations:
```bash
dotnet ef database update
```
### 4. Build and run the project
Use the following commands to build and run the project:

```bash
dotnet build
dotnet run
```
### 5. Access the application
Once the project is running, navigate to http://localhost:5000 (or as specified in your launch settings) to access the application.