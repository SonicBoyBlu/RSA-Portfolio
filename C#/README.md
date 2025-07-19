![License: Personal Use Only](https://img.shields.io/badge/license-personal--use--only-blue.svg)
![No Contributions](https://img.shields.io/badge/contributions-closed-red.svg)
![Production Use](https://img.shields.io/badge/production%20use-not%20authorized-lightgrey.svg)
![Status: Portfolio](https://img.shields.io/badge/status-portfolio-brightgreen.svg)

# C# Projects

This directory contains a collection of backend and API projects built with .NET and C#. Each subfolder represents a distinct solution or service, designed to demonstrate a range of enterprise and microservice patterns.

---

## Project Overview

### API.Finance

A RESTful API providing financial data and portfolio management features.  
**Key Features:**

- Endpoints for retrieving and updating financial records
- Portfolio analytics and reporting
- Authentication and authorization

### API.Global

A shared API exposing global resources and utilities used across multiple services.  
**Key Features:**

- Common data models and endpoints
- Centralized logging and error handling
- Shared authentication mechanisms

### API.v3

The third major version of the core API, featuring improved architecture and new endpoints.  
**Key Features:**

- Modular service structure
- Enhanced performance and scalability
- Backward compatibility with previous API versions

### Blazor.Comms

A real-time communications application built with Blazor.  
**Key Features:**

- Live chat and messaging
- SignalR integration for real-time updates
- User presence and notifications

### Micro.Jobs

A microservice dedicated to job management and background processing.  
**Key Features:**

- Job queueing and scheduling
- Status tracking and retry logic
- Integration with other microservices

### Micro.Signup

A microservice focused on user registration and onboarding workflows.  
**Key Features:**

- User signup and email verification
- Integration with authentication providers
- Secure data handling

### Portal.Admin

An administration portal for managing users, roles, and system settings.  
**Key Features:**

- Role-based access control
- User and permission management
- System configuration dashboard

### Portal.PM.Partner

A partner-facing portal for project management and collaboration.  
**Key Features:**

- Project and task tracking
- Partner onboarding and management
- Collaboration tools and notifications

---

## Getting Started

1. Open the relevant `.sln` file in Visual Studio (e.g., `API.Finance/API.sln`).
2. Restore NuGet packages.
3. Build the solution.
4. Run the project using Visual Studio or the .NET CLI.

### .NET CLI Example

```sh
cd API.Finance
dotnet restore
dotnet build
dotnet run
```

## Requirements

- [.NET SDK](https://dotnet.microsoft.com/download)
- Visual Studio 2022 or later (recommended)

## Contributing

See the root [CONTRIBUTING.md](../CONTRIBUTING.md) for guidelines.

## License

Refer to each project folder for license
