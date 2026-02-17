

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

## Environment Variables (.env)

The project uses a `.env` file for JWT configuration.

Example `.env` file:

JWT_KEY=ULTRA_SECURE_RANDOM_SECRET_8f3KzXP9LmQaZ7sTyBvH4eR6wN0uC
JWT_ISSUER=Auth.Api
JWT_AUDIENCE=AngularClient
JWT_ACCESS_TOKEN_MINUTES=120

---

### Explanation

- JWT_KEY – Secret key used to sign JWT tokens
- JWT_ISSUER – Token issuer
- JWT_AUDIENCE – Intended audience
- JWT_ACCESS_TOKEN_MINUTES – Access token expiration time in minutes

---

### Important

The `.env` file should NOT be committed to source control.

Add it to `.gitignore`:

.env

---

### Creating Your Own .env

Create a file named `.env` inside the `Auth.Api` project folder and define your own secure JWT key.

