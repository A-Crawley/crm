# CRM

A modular ASP.NET Core solution for a Customer Relationship Management (CRM) system. The solution is organized into domain, application, infrastructure, and web API layers with a focus on clean boundaries, testability, and maintainability.

## Tech Stack

- .NET 9.0 / C# 13.0
- ASP.NET Core API
- Domain-Driven Design style layering
- Docker Compose for local development
- Entity Framework or alternative database access (as defined by your Infrastructure/Database projects)

## Project Structure

- Domain
    - Core domain entities, value objects, and domain services.
- Application
    - Use cases, services, and application logic driving the domain.
- Infrastructure
    - Implementations for repositories, external services, data access, and other infrastructure concerns.
- WebApi
    - ASP.NET Core Web API project exposing REST endpoints.
- Database
    - Database migrations, schemas, and seeding logic.
- compose.yaml
    - Docker Compose configuration for running the app stack locally.

Note: The solution file (CRM.sln) references the following projects:
- WebApi
- Application
- Infrastructure
- Domain

## Getting Started

These instructions will help you get a local development environment up and running.

Prerequisites:
- .NET 9.0 SDK or runtime
- Docker and Docker Compose
- (Optional) Docker Desktop for Windows/macOS

1. Clone the repository
    - git clone https://github.com/A-Crawley/crm.git
    - cd CRM

2. Build the solution
    - dotnet restore
    - dotnet build

3. Run with Docker Compose (recommended for local development)
    - Ensure Docker is running
    - docker compose up --build -d
    - The WebApi service will be available at the configured URL (http://localhost:8080)
    - To stop the services: docker compose down --rmi local -v

Alternative: Run the WebApi project directly (without Docker)
- Open the WebApi project in your IDE (e.g., Visual Studio or VS Code)
- Restore and run the WebApi project
- Ensure environment variables and connection strings are configured for your local database

## Configuration

- Environment-specific settings live in appsettings.{Environment}.json within the WebApi project.
- Connection strings and sensitive data should be provided via user secrets during local development or via a secure configuration provider in production.

## Development Practices

- Layered architecture with clear separation of concerns:
    - Domain: business rules and entities
    - Application: use cases and orchestration
    - Infrastructure: data access and external services
    - WebApi: HTTP API and controllers
- Dependency Injection is used throughout to promote testability and loose coupling.
- Project references in the solution ensure proper build order and modularity.

## Testing

- Unit tests: located in tests projects corresponding to Application/Domain layers (add/update as per your convention).
- Integration tests: consider adding an integration test project that exercises the WebApi endpoints against an in-memory or test database.

## Contributing

- Fork the repository
- Create a feature branch
- Implement changes with tests
- Open a pull request
- Follow the project’s coding standards and review guidelines

## Troubleshooting

- If Docker cannot start, ensure the daemon is running and the compose.yaml is properly configured for your environment.
- If database migrations fail, verify connection strings and database permissions.
- Check logs from Docker or the WebApi service for detailed error information.

## License

This project is available under the terms of the LICENSE file in the repository root. If no license is present, please add an appropriate open-source license or contact the repository owner.

---

If you’d like a tailored README with more details (e.g., API endpoints, domain model overview, or contribution guidelines), share any specific requirements or architectural notes, and I’ll incorporate them.