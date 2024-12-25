
# LoanApiCommSchool

## Introduction

LoanApiCommSchool is a backend application designed to provide a RESTful API for managing loans within a communication school context. The project incorporates authentication using JSON Web Tokens (JWT), database integration with Entity Framework Core, and API documentation using Swagger.

---

## Table of Contents

- [Introduction](#introduction)
- [Features](#features)
- [Installation](#installation)
- [Usage](#usage)
- [Configuration](#configuration)
- [Dependencies](#dependencies)
- [API Documentation](#api-documentation)
- [Contributing](#contributing)
- [License](#license)

---

## Features

- **JWT Authentication**:
  - Implements secure authentication using JSON Web Tokens.
  - Configurable token settings (issuer, audience, and expiration).

- **Database Integration**:
  - Uses SQL Server as the database.
  - Configured through Entity Framework Core with a connection string.

- **Swagger Documentation**:
  - Comprehensive API documentation and testing interface.
  - Supports JWT token input for secured API endpoints.

- **Environment-Based Configuration**:
  - Separate configurations for development and production.

---

## Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/Sandrikam/LoanApi.git
   cd LoanApiCommSchool
   ```

2. Install required dependencies using .NET CLI:
   ```bash
   dotnet restore
   ```

3. Set up the database:
   - Update the connection string in `appsettings.json` to match your SQL Server instance.
   - Run the database script (`GenerateDBScript.sql`) to create the necessary schema.

4. Build the project:
   ```bash
   dotnet build
   ```

---

## Usage

1. Run the application:
   ```bash
   dotnet run
   ```

2. Access the API:
   - Base URL: `https://localhost:5001` (or as configured).
   - Swagger documentation available at `/swagger`.

3. Authentication:
   - Obtain a JWT by logging in (endpoint details in Swagger).
   - Include the JWT in the `Authorization` header for protected endpoints:
     ```
     Authorization: Bearer <token>
     ```

---

## Configuration

- **Logging**:
  - Configurable log levels for various namespaces (`appsettings.Development.json` and `appsettings.json`).

- **JWT Settings**:
  - Defined in `appsettings.json` under `JwtSettings`:
    - `Issuer`: Token issuer.
    - `Audience`: Intended audience for the token.
    - `Key`: Secret key for signing the token.
    - `ExpiresInMinutes`: Token expiration time.

- **Database Connection**:
  - Connection string configured in `appsettings.json` under `ConnectionStrings`.

---

## Dependencies

- **.NET Core 5.0** or higher
- **Entity Framework Core** for database management.
- **Microsoft.AspNetCore.Authentication.JwtBearer** for JWT-based authentication.
- **Swagger (Swashbuckle.AspNetCore)** for API documentation.
- **SQL Server** for the database.

---

## API Documentation

Swagger is integrated to provide an interactive API documentation interface. It supports token-based authentication and provides examples for all endpoints.

- URL: `https://localhost:5001/swagger`
- JWT Input: Use the "Authorize" button in Swagger to include your token.

---

## Contributing

1. Fork the repository.
2. Create a new feature branch:
   ```bash
   git checkout -b feature/your-feature-name
   ```
3. Commit your changes:
   ```bash
   git commit -m "Add your message here"
   ```
4. Push to your branch:
   ```bash
   git push origin feature/your-feature-name
   ```
5. Submit a pull request.

---

## License

This project is licensed under the MIT License. See the LICENSE file for details.

---
