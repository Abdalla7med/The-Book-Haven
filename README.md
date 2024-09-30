### The Book Haven - Library Management System (ASP.NET Core MVC)

## Overview
**The Book Haven** is a library management system built using ASP.NET Core. It allows librarians and members to manage books, track loans, handle returns, and impose penalties for overdue returns. The system includes features such as:
- Book search and categorization.
- Member management.
- Loan and return tracking.
- Automated penalty system for late returns.

## Features
-  **Book Borrowing**: Allows users to borrow books from the library with a predefined loan period and track due dates.
-  **Book Returning**: Users can return borrowed books, with penalties applied for overdue returns.
-  **Penalty Calculation**: Calculates and tracks penalties for books returned after the due date Automatically.
-  **Book Availability**: Updates and tracks the availability of books based on the number of copies available.
-  **Member Management**: Handles basic member information such as adding , removing , and viewing members.
-  **Book Search**: Provides a search functionality for finding available books by title, author, or category.

## Entity Relationships

| Entities           | Relationship Name | Description                                                   |
|--------------------|-------------------|---------------------------------------------------------------|
| Book - Author      | WrittenBy         | A book can be written by one or more authors.                 |
| Book - Category    | BelongsTo         | A book can belong to one category.                            |
| Member - Loan      | Borrows           | A member can borrow multiple loans.                           |
| Book - Loan        | Includes          | A loan can include one book, but a book can be loaned multiple times. |
| Member - Penalty   | Incurs            | A member may incur penalties for late returns.                |
| Loan - Penalty     | ResultsIn         | A loan may result in a penalty if the book is returned late.   |

## Architecture

The project follows the **N-Tier Architecture**, divided into three main layers:

1. **API Layer**: This layer handles HTTP requests, returns responses, and contains controllers for managing requests related to books, members, loans, categories, and penalties.
2. **Business Logic Layer (BLL)**: Implements the business logic for handling library operations. This layer processes data, applies rules, and coordinates the interaction between the API and DAL.
3. **Data Access Layer (DAL)**: Handles all database operations using **Entity Framework Core**, and follows the **Repository Pattern** for data access abstraction.

### Additional Layers
- **Shared Layer**: Contains the DTOs (Data Transfer Objects) and AutoMapper profiles used for object mapping between the API and the database.
  
## Design Patterns
### 1. Repository Pattern
The **Repository Pattern** abstracts the data access logic from the business logic, making it easier to manage and modify data access without affecting other layers. Each entity has its own repository interface and implementation, providing a clean way to manage database operations.

### 2. Unit of Work Pattern
The **Unit of Work Pattern** ensures that changes made to the database in a transaction are coordinated across multiple repositories, preventing partial updates and ensuring data consistency.

### Project Structure
```bash
The-Book-Haven/
│
├── API
│   ├── Controllers
│   └── Mappings
│
├── BLL
│   ├── Services
│   └── Business Logic
│
├── DAL
│   ├── Repositories
│   ├── UnitOfWork
│   └── DbContext
│  
└── Shared
    └── DTOs
```

## Technology Stack
- **Backend**: ASP.NET Core MVC, Entity Framework Core
- **Database**: SQL Server
- **Frontend**: HTML, CSS, JavaScript, Razor Views
- **Version Control**: Git

## Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/Abdalla7med/The-Book-Haven.git

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

### How to Use
To get a detailed understanding of how to use the system, refer to the[User Manual](./Manual.md)
file.

### Basic Operations:
**Search Books**: Use the search bar to find books by title, author, or category.
**Borrow a Book**: Navigate to the desired book and click "Borrow." Select the member to assign the loan.
**Return a Book**: Navigate to the member's loan list and select "Return" to register the return and check for penalties.
**Penalties**: The system will automatically calculate penalties for overdue returns and notify the member.

### Contributing
Feel free to fork this repository and submit pull requests to contribute new features or fix bugs. Make sure to follow the contributing guidelines.

### License
This project is licensed under the MIT License - see the LICENSE file for details.

### Contact
For any inquiries or issues, you can reach me at:

GitHub: Abdalla7med
Email: abdullah.sherdy.work@gmail.com