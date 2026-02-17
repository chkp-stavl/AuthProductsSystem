

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

