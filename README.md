

## Database Setup

The repository contains two SQL files:

- database-schema.sql
- seed-data.sql

These scripts allow recreating the database structure and initial data without committing database files.

### Step 1 – Create Database

Run in SQL Server:

CREATE DATABASE AuthProductsDb;

### Step 2 – Run Schema Script

Execute:

database-schema.sql

This will create:
- Tables
- Primary Keys
- Foreign Keys
- Indexes

### Step 3 – Run Seed Script

Execute:

seed-data.sql

This will insert:
- Categories
- Users
- Products

After running both scripts, the database is ready.

---

## Environment Configuration

Update the connection string in appsettings.json.

For LocalDB:

"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=AuthProductsDb;Trusted_Connection=True;"
}

For SQL Server:

"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=AuthProductsDb;Trusted_Connection=True;"
}

---

## Running the Application

From the solution root:

dotnet run --project Auth.Api

The API will start locally.

---

## Authentication & Authorization

The system implements:

- JWT authentication stored in HttpOnly cookies
- Password hashing
- Role-based authorization
- Protected endpoints

Seeded roles include:
- Admin
- Viewer

Protected endpoints require valid authentication and appropriate role.

---

## Main API Endpoints

POST /auth/login  
POST /auth/register  
GET /products  
POST /products  
PUT /products/{id}  
DELETE /products/{id}

---

## Database Versioning Strategy

Database files (.mdf) are not committed.  
The database is versioned using:

- database-schema.sql for structure
- seed-data.sql for initial data

This ensures reproducible environment setup and clean repository management.

---

## Future Improvements

- Add Docker support
- Add integration tests
- Add CI/CD pipeline
- Add structured logging
- Add refresh token mechanism
- Add pagination and filtering

---

## Project Goals

- Demonstrate Clean Architecture implementation
- Implement secure authentication
- Show proper database versioning practices
- Build a scalable backend foundation
