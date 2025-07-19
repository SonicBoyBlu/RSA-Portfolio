![License: Personal Use Only](https://img.shields.io/badge/license-personal--use--only-blue.svg)
![No Contributions](https://img.shields.io/badge/contributions-closed-red.svg)
![Production Use](https://img.shields.io/badge/production%20use-not%20authorized-lightgrey.svg)
![Status: Portfolio](https://img.shields.io/badge/status-portfolio-brightgreen.svg)

# BambooHrClient

**BambooHrClient** is a .NET Framework 4.6.1 library for integrating with the BambooHR API. It provides a strongly-typed, extensible client for accessing and managing HR data, including employees, time off, holidays, users, and more. This library is designed for use within larger .NET solutions that require HR data synchronization, reporting, or automation.

---

## Features

- **Employee Management:**  
  Retrieve, add, and update employee records, including directory and payroll hours.

- **Time Off Management:**  
  Create, update, and cancel time off requests; fetch policies, types, and future balance estimates.

- **Photo Handling:**  
  Download and upload employee photos.

- **HR Metadata:**  
  Access fields, tables, and list field details for dynamic HR data structures.

- **WhosOut & Holidays:**  
  Query who is out of office and upcoming holidays.

- **User Management:**  
  Retrieve BambooHR users and their details.

- **Change Tracking:**  
  Fetch employee change history for synchronization.

- **Utility Extensions:**  
  Includes helpers for string manipulation, XML serialization, and REST response handling.

---

## Project Structure

- `BambooHrClient.cs` – Main client implementation and interface
- `Config.cs` – Reads BambooHR API configuration from `App.config`/`Web.config`
- `Constants.cs` – Shared constants (e.g., date formats)
- `TextExtensions.cs` – String and JSON utility extensions
- `Extensions/` –
  - `IRestResponseExtensions.cs` – REST error handling
  - `ObjectExtensions.cs` – Object property stringification
  - `XElementExtensions.cs` – XML serialization helpers
- `Models/` – Strongly-typed models for employees, time off, holidays, users, tables, etc.
- `packages.config` – NuGet dependencies (`Newtonsoft.Json`, `RestSharp`)
- `Properties/` – Assembly info and metadata

---

## Dependencies

- [.NET Framework 4.6.1](https://dotnet.microsoft.com/download/dotnet-framework/net461)
- [RestSharp](https://www.nuget.org/packages/RestSharp/) (for HTTP requests)
- [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/) (for JSON serialization)

---

## Usage

1. **Add Reference:**  
   Add the `BambooHrClient` project or DLL to your .NET solution.

2. **Configure API Access:**  
   Add the following keys to your `App.config` or `Web.config`:

   ```xml
   <appSettings>
     <add key="BambooApiUser" value="your-username" />
     <add key="BambooApiKey" value="your-api-key" />
     <add key="BambooApiUrl" value="https://api.bamboohr.com/api/gateway.php/your-company/v1" />
     <add key="BambooCompanyUrl" value="https://yourcompany.bamboohr.com" />
   </appSettings>
   ```

3. **Example: Fetch Employees**

   ```csharp
   var client = new BambooHrClient.BambooHrClient();
   var employees = await client.GetEmployees();
   ```

4. **Example: Create Time Off Request**
   ```csharp
   int requestId = await client.CreateTimeOffRequest(employeeId, timeOffTypeId, startDate, endDate, comment: "Vacation");
   ```

---

## Development Notes

- All API calls are asynchronous (`Task<T>`).
- Error handling is built-in and will throw exceptions with BambooHR error messages when available.
- Utility extensions are provided for common string, XML, and REST operations.

---

## License

See the root repository for license details.  
Third-party libraries are included under their respective open-
