![License: Personal Use Only](https://img.shields.io/badge/license-personal--use--only-blue.svg)
![No Contributions](https://img.shields.io/badge/contributions-closed-red.svg)
![Production Use](https://img.shields.io/badge/production%20use-not%20authorized-lightgrey.svg)
![Status: Portfolio](https://img.shields.io/badge/status-portfolio-brightgreen.svg)

# PartnerPortal

**PartnerPortal** is an ASP.NET MVC web application for partner and project management, CRM, reporting, and administration. It is part of the Portal.PM.Partner solution and integrates with external services such as BambooHR and SmartTracker. The portal is designed for extensibility, maintainability, and ease of use for business partners and administrators.

---

## Features

- **CRM:** Manage contacts, leads, notes, and activities.
- **Project Management:** Track projects, tasks, and housekeeping operations.
- **Reporting:** Generate operational and housekeeping reports (some features are in development).
- **Admin Tools:** Manage users, roles, and system settings.
- **HR Integration:** Connects with BambooHR for HR data synchronization.
- **External Services:** Integrates with SmartTracker web services for organization and task management.
- **Extensible UI:** Organized into controllers, views, and scripts for maintainability.
- **Plugin Support:** Includes third-party libraries for tooltips, table export, and more.

---

## Folder Structure

- `App_Start/` – Application configuration (bundling, filters, routes, markup minification)
- `Connected Services/` – Integration with external web services (SmartTracker)
- `Content/` – Static assets, styles, icons, and plugins
- `Controllers/` – MVC controllers for business logic and routing
- `Data/` – Data access and mapping
- `Extensions/` – Extension methods for various utilities
- `Filters/` – Custom filters for requests and responses
- `Helpers/` – Helper classes for common tasks
- `Interfaces/` – Abstractions for services and repositories
- `json/` – JSON data and configuration files
- `Methods/` – Business logic and utility methods
- `Models/` – Entity and view models
- `Properties/` – Project properties and assembly info
- `Routes/` – Custom route definitions
- `Scripts/` – JavaScript libraries and custom scripts ([see detailed README](Scripts/README.md))
- `Services/` – Service classes for business logic and integration
- `Settings/` – Application settings and configuration
- `ViewModels/` – View models for passing data to views
- `Views/` – Razor views for UI rendering

---

## Getting Started

### Prerequisites

- [.NET Framework](https://dotnet.microsoft.com/) (version as specified in project files)
- Visual Studio 2022+ (recommended)
- Access to BambooHR and SmartTracker APIs (for full integration)

### Setup

1. Open the solution in Visual Studio.
2. Restore NuGet packages.
3. Configure connection strings and API keys in `Web.config` and related config files.
4. Build and run the solution.

### Running the Application

- The main entry point is `Global.asax`.
- Access the portal via the configured local IIS Express or web server URL.

---

## Development Notes

- **Scripts:**  
  See [`Scripts/README.md`](Scripts/README.md) for details on JavaScript libraries (e.g., Popper.js, Tooltip.js) and usage.
- **Table Export:**  
  Table export features use plugins documented in [`Content/plugins/bootstrap-table/plugins/tableExport/README.md`](Content/plugins/bootstrap-table/plugins/tableExport/README.md).
- **Icons:**  
  Apple touch icons and favicons are generated using [iconifier.net](http://iconifier.net/readme).
- **UI Customization:**  
  Modify views in `Views/` and static assets in `Content/` for branding or feature changes.

---

## License

See the root repository for license details.  
Third-party libraries are included under their respective open-source
