![License: Personal Use Only](https://img.shields.io/badge/license-personal--use--only-blue.svg)
![No Contributions](https://img.shields.io/badge/contributions-closed-red.svg)
![Production Use](https://img.shields.io/badge/production%20use-not%20authorized-lightgrey.svg)
![Status: Portfolio](https://img.shields.io/badge/status-portfolio-brightgreen.svg)

# Portal.PM.Partner

**Portal.PM.Partner** is a comprehensive .NET web solution for partner and project management, integrating HR data, CRM, reporting, and administrative tools. It is organized into two main components: a BambooHR integration library and the main Partner Portal web application.

---

## Folder Structure

```
BambooHrClient/
  ├── BambooHrClient.csproj
  ├── BambooHrClient.cs
  ├── Config.cs
  ├── Constants.cs
  ├── TextExtensions.cs
  ├── Extensions/
  ├── Models/
  └── ...
PartnerPortal/
  ├── Global.asax
  ├── Web.config
  ├── App_Start/
  ├── Content/
  ├── Controllers/
  ├── Data/
  ├── Extensions/
  ├── Filters/
  ├── Helpers/
  ├── Interfaces/
  ├── Methods/
  ├── Models/
  ├── Routes/
  ├── Scripts/
  ├── Services/
  └── ...
```

---

## Components

### BambooHrClient

A .NET library for integrating with the BambooHR API, providing:

- HR data access and synchronization
- Configuration and constants for API integration
- Extension methods and models for HR data handling

### PartnerPortal

An ASP.NET MVC web application for partner and project management, featuring:

- **CRM:** Manage contacts, leads, notes, and activities
- **Reports:** Generate and view various operational and housekeeping reports
- **Admin Tools:** Manage users, roles, and system settings
- **Data Integration:** Connects with BambooHR and other external services
- **Extensible Architecture:** Organized into controllers, services, helpers, and more for maintainability

---

## Key Features

- **HR Integration:** Sync and display HR data from BambooHR
- **CRM Functionality:** Track leads, contacts, notes, and activities
- **Reporting:** Generate operational and housekeeping reports (some features marked as "Coming Soon")
- **Admin & Security:** Role-based access and system configuration
- **Extensible Plugins:** Includes third-party libraries (e.g., Popper.js, Bootstrap Table Export) for enhanced UI and data export

---

## Getting Started

### Prerequisites

- [.NET Framework](https://dotnet.microsoft.com/) (version as specified in project files)
- Visual Studio 2022+ (recommended)
- Access to BambooHR API (for HR integration features)

### Setup

1. Open the solution in Visual Studio.
2. Restore NuGet packages.
3. Configure connection strings and API keys in `Web.config` and `Config.cs` as needed.
4. Build and run the solution.

### Running the Application

- The main entry point is `PartnerPortal/Global.asax`.
- Access the web portal via the configured local IIS Express or web server URL.

---

## Development Notes

- **Scripts and Plugins:**  
  See [`PartnerPortal/Scripts/README.md`](PartnerPortal/Scripts/README.md) for details on JavaScript libraries and usage.
- **Export Features:**  
  Table export and reporting use plugins documented in their respective folders.
- **UI Customization:**  
  Modify views in `PartnerPortal/Views/` and static assets in `Content/`.

---

## License

See the root repository for license details.  
Third-party libraries are included under their respective licenses.
