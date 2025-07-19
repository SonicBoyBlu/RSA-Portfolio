![License: Personal Use Only](https://img.shields.io/badge/license-personal--use--only-blue.svg)
![No Contributions](https://img.shields.io/badge/contributions-closed-red.svg)
![Production Use](https://img.shields.io/badge/production%20use-not%20authorized-lightgrey.svg)
![Status: Portfolio](https://img.shields.io/badge/status-portfolio-brightgreen.svg)

# Micro.Jobs

**Micro.Jobs** is a .NET web application for job listings and employer/candidate management. It demonstrates a hybrid approach using both Razor Pages and Blazor components, and is designed for extensibility, modern UI, and integration with external platforms.

---

## Features

- **Job Listings:**  
  Browse, search, and manage job postings.

- **Employer & Candidate Management:**  
  Pages for employers and candidates to manage their profiles and job applications.

- **Modern UI:**  
  Responsive layout with dynamic logo theming (e.g., holiday and pride logos), mobile-friendly meta tags, and custom styling.

- **Blazor Integration:**  
  Uses Blazor components for interactive client-side experiences alongside traditional Razor Pages.

- **Custom Forms:**  
  Includes styled forms for user input and messaging.

- **Extensible Architecture:**  
  Modular structure for easy addition of new features and pages.

- **CI/CD Ready:**  
  Includes `azure-pipelines.yml` for Azure DevOps integration.

---

## Project Structure

- `Components/`
  - Blazor components and layouts (`App.razor`, `Routes.razor`, `Pages/`, `Layout/`)
- `Pages/`
  - Razor Pages for jobs, employers, categories, error handling, and more
- `wwwroot/`
  - Static assets (CSS, images, icons)
- `Program.cs`
  - Application entry point and configuration
- `Root.cs`
  - Application-wide settings and helpers
- `appsettings.json` / `appsettings.Development.json`
  - Configuration files for environment-specific settings
- `azure-pipelines.yml`
  - Azure DevOps pipeline definition

---

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- Visual Studio 2022+ or VS Code

### Running the Application

1. **Navigate to the project folder:**

   ```sh
   cd "C#/Micro.Jobs"
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

- **Dynamic Logo:**  
  The logo changes based on the month (holiday and pride themes) in the shared layout.
- **UI Customization:**  
  Modify Blazor components in `Components/` and Razor Pages in `Pages/` for new features or UI changes.
- **CI/CD:**  
  Edit `azure-pipelines.yml` for automated builds and deployments.

---

## License

See the root repository for license details.
