![License: Personal Use Only](https://img.shields.io/badge/license-personal--use--only-blue.svg)
![No Contributions](https://img.shields.io/badge/contributions-closed-red.svg)
![Production Use](https://img.shields.io/badge/production%20use-not%20authorized-lightgrey.svg)
![Status: Portfolio](https://img.shields.io/badge/status-portfolio-brightgreen.svg)

# Blazor.Comms

**Blazor.Comms** is a real-time communication hub built with ASP.NET Core Blazor Server. It demonstrates SignalR-based messaging, scheduled notifications, telemetry, and integration with AWS SES for email—all within a modular, extensible architecture.

---

## Features

- **Real-Time Console:**  
  SignalR-powered console for live system and user messages.

- **FSI Notifications:**  
  Scheduled reminders and notifications for users, with support for custom scheduling and logging.

- **Telemetry & Logging:**  
  Centralized logging and telemetry services for system events and user actions.

- **Email Integration:**  
  AWS SES integration for sending emails programmatically.

- **Responsive UI:**  
  Blazor Server UI with Bootstrap styling and Open Iconic icons.

- **Sample Data & Pages:**  
  Includes weather forecast demo, counter, and error pages.

---

## Project Structure

- `Actions/` – Notification and action logic (e.g., FSI notifications)
- `Data/` – Data models and services (e.g., weather forecast)
- `Models/` – Entity and DTO classes (e.g., recipients, run logs)
- `Pages/` – Razor pages and components (console, FSI, error, fetch data, etc.)
- `Services/` – Email, telemetry, and utility services
- `Settings/` – Application and hub configuration
- `Shared/` – Shared UI components and layouts
- `Tasks/` – Background and scheduled task logic (e.g., FSI scheduler)
- `wwwroot/` – Static files (CSS, icons, etc.)
- `Program.cs` – Application entry point and service configuration

---

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- Visual Studio 2022+ or VS Code

### Running the App

1. **Navigate to the project folder:**

   ```sh
   cd "C#/Blazor.Comms"
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
   Visit the URL shown in the terminal (e.g., [https://localhost:7085](https://localhost:7085)).

---

## Development Notes

- **SignalR Hubs:**  
  Console and FSI hubs are initialized at startup for real-time communication.
- **Configuration:**  
  Edit `Settings/Settings.cs` and `Properties/launchSettings.json` for environment and hub URLs.
- **Email:**  
  AWS credentials and region must be set in your environment or configuration for email to work.
- **UI Customization:**  
  Modify components in `Shared/` and `Pages/` for layout and navigation.

---

## License

See the root repository for license details.  
Open Iconic icons are licensed under MIT and SIL OFL (see `wwwroot/css/open-iconic/`).
