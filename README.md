
## API Endpoints

The system exposes REST APIs for authentication and product management.

### Authentication

**POST `/api/auth/login`**  
Authenticates a user using username and password.  
On success, a JWT token is generated and stored in an HttpOnly cookie.  
Returns basic user information and permission flag.

**GET `/api/auth/me`**  
Returns information about the currently authenticated user.  
Requires a valid JWT cookie.

**POST `/api/auth/logout`**  
Clears the authentication cookie and logs the user out.

---

### Products

**GET `/api/products`**  
Returns a list of products.  
Supports optional query parameters: `name`, `categoryId`.  
Authentication required.

**GET `/api/products/{id}`**  
Returns a product by its ID.  
Authentication required.

**POST `/api/products`**  
Creates a new product.  
Requires `Admin` role.

**PUT `/api/products/{id}`**  
Updates an existing product.  
Requires `Admin` role.

**DELETE `/api/products/{id}`**  
Deletes a product.  
Requires `Admin` role.

**GET `/api/products/form-data`**  
Returns supporting data (such as categories) required for product creation forms.  
Authentication required.

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

