![License: Personal Use Only](https://img.shields.io/badge/license-personal--use--only-blue.svg)
![No Contributions](https://img.shields.io/badge/contributions-closed-red.svg)
![Production Use](https://img.shields.io/badge/production%20use-not%20authorized-lightgrey.svg)
![Status: Portfolio](https://img.shields.io/badge/status-portfolio-brightgreen.svg)

# API.v3

API.v3 is an ASP.NET Core MVC web application that serves as the third major version of the core API and web interface for this portfolio. It demonstrates modern .NET 8 project structure, MVC patterns, and integration with popular frontend libraries.

---

## Features

- **ASP.NET Core MVC:**  
  Implements the Model-View-Controller pattern for clean separation of concerns.

- **Razor Views:**  
  Uses Razor syntax for dynamic server-side rendering of HTML.

- **Static Assets:**  
  Includes Bootstrap, jQuery, and validation libraries for responsive and interactive UI.

- **Error Handling:**  
  Built-in error view and model for robust error reporting.

- **Environment Configuration:**  
  Supports development and production environments via `launchSettings.json`.

---

## Project Structure

- `Controllers/`  
  Contains MVC controllers (e.g., `HomeController.cs`) that handle HTTP requests and return views or data.

- `Models/`  
  View models and data classes (e.g., `ErrorViewModel.cs`).

- `Views/`  
  Razor views for rendering HTML.

  - `Home/` – Main pages (`Index.cshtml`, `Privacy.cshtml`)
  - `Shared/` – Layout, error, and partial views

- `wwwroot/`  
  Static files served directly (CSS, JS, images, and third-party libraries like Bootstrap and jQuery).

- `Properties/`  
  Project settings, including `launchSettings.json` for environment and launch configuration.

- `Program.cs`  
  Application entry point and middleware configuration.

---

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- Visual Studio 2022+ or Visual Studio Code

### Running the Application

1. **Navigate to the project folder:**

   ```sh
   cd "C#/API.v3"
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
   Visit the URL shown in the terminal (e.g., [https://localhost:7214](https://localhost:7214) or [http://localhost:5183](http://localhost:5183)).

---

## Development Notes

- **Static Assets:**  
  Modify files in `wwwroot/` for custom styles or scripts.

- **Views:**  
  Update Razor files in `Views/` to change UI or add new pages.

- **Controllers:**  
  Add new controllers in `Controllers/` to expand API or web functionality.

- **Configuration:**  
  Adjust `launchSettings.json` for custom ports or environment variables.

---

## License

See the root repository for license details.  
Third-party libraries (Bootstrap, jQuery, etc.) are included under their respective open-source licenses.
