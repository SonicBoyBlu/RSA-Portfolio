![License: Personal Use Only](https://img.shields.io/badge/license-personal--use--only-blue.svg)
![No Contributions](https://img.shields.io/badge/contributions-closed-red.svg)
![Production Use](https://img.shields.io/badge/production%20use-not%20authorized-lightgrey.svg)
![Status: Portfolio](https://img.shields.io/badge/status-portfolio-brightgreen.svg)

# Portal.Admin

**Portal.Admin** is a .NET 8 Razor Pages web application for administration of users, candidates, locations, and system settings. It provides a secure, responsive interface for managing core data and configurations across the platform.

---

## Features

- **User & Candidate Management:**  
  Administer user accounts and candidate records.

- **Location Management:**  
  Manage location data such as countries, states, and regions.

- **System Settings:**  
  Configure application-wide settings via `appsettings.json` and `appsettings.Development.json`.

- **Responsive UI:**  
  Mobile-friendly design with custom icons and theming.

- **Error Handling:**  
  Custom error pages and robust error reporting.

---

## Project Structure

- `Pages/`

  - `Index.cshtml` – Admin dashboard
  - `Privacy.cshtml` – Privacy policy
  - `Error.cshtml` – Error handling
  - `Candidates/` – Candidate management pages
  - `Locations/` – Location management pages
  - `Shared/` – Shared layouts and partials
  - `_ViewImports.cshtml`, `_ViewStart.cshtml` – Razor configuration

- `wwwroot/`

  - Static assets, icons, and styles

- `Program.cs`

  - Application entry point and configuration

- `appsettings.json` / `appsettings.Development.json`

  - Application and environment-specific settings

- `compilerconfig.json`
  - SCSS/CSS compilation configuration

---

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- Visual Studio 2022+ or VS Code

### Running the Application

1. **Navigate to the project folder:**

   ```sh
   cd "C#/Portal.Admin"
   ```

2. **Restore dependencies:**

   ```sh
   dotnet restore
   ```

3. **Run the application:**

   ```sh
   dotnet run
   ```

4. **Open your browser:**  
   Visit the URL shown in the terminal (e.g., [https://localhost:5001](https://localhost:5001)).

---

## Development Notes

- **UI Customization:**  
  Modify Razor Pages in `Pages/` and static assets in `wwwroot/` for branding or feature changes.

- **Configuration:**  
  Adjust `appsettings.json` for system settings and environment variables.

- **SCSS Compilation:**  
  Styles are managed via `compilerconfig.json` for SCSS to CSS compilation.

---

## License

See the root repository for license
