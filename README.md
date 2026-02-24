# BemConecto API

The **BemConecto API** is the official backend for the BemConecto system — a platform designed to support psychologists in managing their practice, including patient registration, appointment scheduling, and session tracking.

Built with **.NET 8 (ASP.NET Core Web API)**, the API follows a modular architecture, strong validation patterns, and secure authentication using **JWT**.

## ✨ Overview

The API provides the necessary resources for psychologists and clinics to manage their workflow in a centralized and efficient way.

It is designed to integrate with frontend applications, exposing RESTful endpoints for authentication and CRUD operations.

## 🧱 Tech Stack

- .NET 8 – Backend framework
- C# – Main programming language
- ASP.NET Core Web API – REST API framework
- Entity Framework Core – ORM and database migrations
- PostgreSQL – Relational database
- JWT Authentication – Token-based security
- FluentValidation – Input validation
- xUnit / NUnit – Testing framework (optional future setup)

## 📂 Project Structure (Suggested)

src/
├── BemConecto.Api/ → Presentation layer (Controllers, Middleware, Configuration)
├── BemConecto.Application/ → Application business rules and use cases
├── BemConecto.Domain/ → Domain entities and interfaces
├── BemConecto.Infrastructure/ → Database, repositories, external services
└── BemConecto.Tests/ → Automated tests


This structure follows Clean Architecture principles, promoting scalability and maintainability.

## ⚙️ Installation and Setup

Clone the repository:

```bash
git clone https://github.com/rodrigocnn/bemconecto-api.git
```
Enter the project directory:

```bash
cd bemconecto-api
```
Restore dependencies:

```bash
dotnet restore
```
🔧 Environment Configuration

Configure connection strings and secrets in appsettings.json or .env:

```bash
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=bemconecto;Username=user;Password=password"
  },
  "Jwt": {
    "Secret": "your_secret_key"
  }
}
```

🗃️ Database Migrations

Run migrations:

```bash
dotnet ef database update
```

▶️ Running the Project

Development mode:

```bash
dotnet run
```

Build project:

```bash
dotnet build
```
