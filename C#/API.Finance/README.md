![License: Personal Use Only](https://img.shields.io/badge/license-personal--use--only-blue.svg)
![No Contributions](https://img.shields.io/badge/contributions-closed-red.svg)
![Production Use](https://img.shields.io/badge/production%20use-not%20authorized-lightgrey.svg)
![Status: Portfolio](https://img.shields.io/badge/status-portfolio-brightgreen.svg)

# API.Finance

API.Finance is a RESTful .NET backend service for financial data and portfolio management. It provides endpoints for managing stocks, comments, and related financial operations, and is designed for extensibility and integration with other services.

---

## Features

- **Stock Management:**  
  CRUD operations for stocks and related data.

- **Comment System:**  
  Endpoints for adding and retrieving comments on stocks.

- **Database Integration:**  
  Uses Entity Framework Core with SQLite (`FinShark.db`) for data persistence.

- **API Documentation:**  
  Integrated Swagger UI for exploring and testing endpoints.

- **Environment Configuration:**  
  Supports multiple environments via `appsettings.json` and `appsettings.Development.json`.

---

## Project Structure

- `Controllers/` – API controllers for handling HTTP requests (e.g., `StockController.cs`, `CommentController.cs`)
- `Data/` – Database context and seed data (`ApplicationDBContext.cs`, `FinShark.db`)
- `Interfaces/` – Repository interfaces for abstraction
- `Repositories/` – Implementation of data access logic
- `Models/` – Entity and DTO classes
- `Mappers/` – Mapping logic between models and DTOs
- `Migrations/` – Entity Framework Core migrations
- `Swagger/` – Swagger configuration for API docs
- `wwwroot/` – Static files (if any)
- `Program.cs` – Application entry point and configuration

---

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- Visual Studio 2022+ or VS Code

### Setup

1. **Clone the repository** and navigate to this folder:

   ```sh
   cd C#/API.Finance
   ```

2. **Restore dependencies:**

   ```sh
   dotnet restore
   ```

3. **Apply migrations (if needed):**

   ```sh
   dotnet ef database update
   ```

4. **Run the API:**

   ```sh
   dotnet run
   ```

5. **Access Swagger UI:**  
   Visit [http://localhost:5000/swagger](http://localhost:5000/swagger) (or the port shown in your terminal) to explore the API.

---

## Development

- **Configuration:**  
  Edit `appsettings.json` or `appsettings.Development.json` for environment-specific settings.
- **Database:**  
  The default setup uses SQLite (`FinShark.db`). You can change the connection string in `appsettings.json`.
- **Testing:**  
  Add or run unit/integration tests as needed (not included by default).

---

## License

See the root repository for license details.

---
