![License: Personal Use Only](https://img.shields.io/badge/license-personal--use--only-blue.svg)
![No Contributions](https://img.shields.io/badge/contributions-closed-red.svg)
![Production Use](https://img.shields.io/badge/production%20use-not%20authorized-lightgrey.svg)
![Status: Portfolio](https://img.shields.io/badge/status-portfolio-brightgreen.svg)

# API.Global

API.Global is a foundational .NET library providing shared data types, utilities, and core business logic for other services and APIs in the portfolio. It centralizes common functionality such as security, settings, user and admin management, candidate and company data, events, jobs, locations, and more.

---

## Features

- **Shared Data Types:**  
  Common models and response types for consistent data handling across services.

- **Security & Settings:**  
  Centralized security logic and application settings management.

- **User & Admin Management:**  
  Structures and utilities for handling users and administrators.

- **Candidate & Company Modules:**  
  Classes and utilities for managing candidate and company data, including accounts, search, and details.

- **Event & Job Management:**  
  Logic for handling events, signups, jobs, and related models.

- **Location Data:**  
  Country and state data, interfaces, and models for location-based features.

- **Utilities:**  
  Helper functions and general-purpose utilities for use throughout the codebase.

---

## Project Structure

- **Root Files:**

  - `DataTypes.cs` – Common data types and models
  - `Security.cs` – Security-related logic
  - `Settings.cs` – Application settings and configuration
  - `Utilities.cs` – General utility functions

- **Admin/**

  - `Users/` – Admin user management

- **Candidates/**

  - `Candidate.Accounts.cs`, `Candidates.Accounts.Utilities.cs` – Candidate account logic
  - `SearchBasic.cs` – Basic candidate search
  - `Details/`, `Models/` – Candidate details and data models

- **Companies/**

  - `Models/` – Company data models

- **Data/**

  - `GeneralResponse.cs` – Standardized API responses
  - `SQL/` – SQL-related resources

- **Events/**

  - `Events.cs`, `Signups.cs` – Event and signup logic
  - `Models/` – Event data models

- **Jobs/**

  - `Jobs.cs` – Job management logic
  - `Models/` – Job data models

- **Locations/**

  - `Country.cs`, `States.cs` – Country and state data
  - `Interfaces/`, `Models/` – Location interfaces and models

- **Resumes/**

  - Resume-related logic and models

- **Search/**

  - Search utilities and logic

- **Users/**

  - User management logic

- **Properties/**
  - `.resx` resource files for localization and resources

---

## Usage

Add `API.Global` as a project reference in your .NET solutions to access shared types, utilities, and business logic.

### Example (in `.csproj`):

```xml
<ItemGroup>
  <ProjectReference Include="..\API.Global\Global.csproj" />
</ItemGroup>
```

---

## Development

- Requires [.NET SDK](https://dotnet.microsoft.com/download)
- Build with Visual Studio or `dotnet build`
- Extend or modify modules as needed for your services

---

## License

See the root repository for
