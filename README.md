# Api_project
# Dotnet WebAPI Project

## Overview

This project is a .NET WebAPI designed with REST architecture, adhering to REST principles.
It includes a well-structured use of status codes and is divided into layers,
each with specific responsibilities.

API Documentation
The API is documented using Swagger. You can access the API documentation via the following link:
https://localhost:7147/swagger/index.html

Project Structure

The project is divided into several layers:

Presentation Layer (Controllers) - Handles HTTP requests and responses.
Application Layer (Services, DTOs) - Contains business logic and manages Data Transfer Objects (DTOs).
Domain Layer (Entities, Interfaces) - Represents the core domain logic and entity definitions.
Infrastructure Layer (Database, Repositories, Registry, Configuration) - Handles persistence, registry, and configurations.

Why Layers?
Separation of Concerns - Each layer has distinct responsibilities.
Scalability - The architecture supports adding new features without affecting existing code.
Testability - Layers make it easier to write unit and integration tests.

### Interacting with Layers

Layers communicate with each other using dependency injection.
This approach ensures that logic is written once and can be injected wherever needed in the project.
Additionally, dependency injection allows for efficient project management; if a code change is needed, it can be done in one place. The logic for dependency injection is written in the `program.cs` file.

## Data Transfer Objects (DTOs)

The project uses DTOs defined as records because the objects do not need to be modified. The advantages of using DTOs include:

1. Removing circular dependencies between objects.
2. Allow partial return or acceptance of objects for security purposes etc.

Conversion between layers using DTOs is done using AutoMapper

Asynchronous processing: Implemented using async/await for scalability and performance.

SQL database with code-first access: The database is managed using Entity Framework Core with migrations.

Configuration management: Application settings are managed using configuration files.

Global Error Handling (Middleware): Errors are logged and fatal errors are sent via email.

Request Logging: Every request is logged for analysis and ranking purposes.

Database Configuration
To configure the database, use the following commands:

# Add Migration
dotnet ef migrations add InitialCreate

# Update Database
Update dotnet ef database
Run the project
Duplicate the repository.
Restore dependencies:
dotnet restore
Run the application:
dotnet run

Clean Code Principles
The project follows clean code principles to ensure maintainability:
Meaningful variable and method names.
Separation of concerns.
Avoid magic numbers and hard-coded values.
Proper exception handling.

## License

This project is licensed under the MIT License.