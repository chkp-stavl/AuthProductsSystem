# Auth Products System

Author: Stav Lidor  
GitHub: https://github.com/chkp-stavl  

A Clean Architecture ASP.NET Core Web API for managing products with:

- .NET 8
- SQL Server (LocalDB / MSSQL)
- Entity Framework Core
- JWT Authentication (Cookie-based)
- Role-based Authorization

---

## 🏗 Architecture

The solution follows Clean Architecture principles:

- **Auth.Api** – Controllers & Presentation layer
- **Auth.Core** – Domain entities & enums
- **Auth.Infrastructure** – DbContext & data access implementation

---

## 🗄 Database Setup

The repository includes SQL scripts to recreate the database:

