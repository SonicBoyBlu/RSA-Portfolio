![License: Personal Use Only](https://img.shields.io/badge/license-personal--use--only-blue.svg)
![No Contributions](https://img.shields.io/badge/contributions-closed-red.svg)
![Production Use](https://img.shields.io/badge/production%20use-not%20authorized-lightgrey.svg)
![Status: Portfolio](https://img.shields.io/badge/status-portfolio-brightgreen.svg)

# Micro.Signup

**Micro.Signup** is a lightweight .NET 8 Razor Pages web application for managing event signups, such as career fairs or job events. It provides a streamlined, mobile-friendly interface for candidates to register, and for administrators to review and manage signups.

---

## Features

- **Event Selection:**  
  Admins can select an event and enroller before opening the signup form.

- **Candidate Signup:**  
  Candidates enter their information (name, phone, email, graduation date, major, etc.) via a guided, carousel-style form.

- **Admin Review:**  
  After candidate submission, admins can review, add notes, and approve signups.

- **Dynamic UI:**  
  Responsive design with Bootstrap, custom SCSS, and dynamic logo theming (holiday/pride).

- **Client-Side Validation:**  
  Uses jQuery Validation and Inputmask for real-time form validation and input formatting.

- **Local Storage:**  
  Form state and event selection are persisted in browser localStorage for a smooth user experience.

- **Privacy Policy:**  
  Dedicated privacy policy page and EULA link.

---

## Project Structure

- `Pages/`

  - `Index.cshtml` – Event selection and admin sign-in
  - `Signup.cshtml` – Candidate signup form and admin review
  - `Privacy.cshtml` – Privacy policy
  - `Clear.cshtml` – Clears local storage and resets the app
  - `Shared/_Layout.cshtml` – Main layout and navigation

- `wwwroot/`

  - `css/` – SCSS and compiled CSS for custom theming
  - `js/` – Custom scripts and plugins (form logic, input masks, serialization)
  - `lib/` – Third-party libraries (Bootstrap, jQuery, jQuery Validation, Inputmask)
  - `app.css` – Additional styling
  - `apple-touch-icon-*.png` – Mobile icons

- `Program.cs` – App startup and configuration
- `Root.cs` – App settings and reload logic
- `appsettings.json` / `appsettings.Development.json` – Configuration files

---

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- Visual Studio 2022+ or VS Code

### Running the Application

1. **Navigate to the project folder:**

   ```sh
   cd "C#/Micro.Signup"
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

- **SCSS Compilation:**  
  SCSS files in `wwwroot/css/` are compiled to CSS via `compilerconfig.json`.

- **Form Logic:**  
  Main client-side logic is in `wwwroot/js/site.js` and `wwwroot/js/plugins/serializeObject.js`.

- **Validation:**  
  Uses jQuery Validation and Inputmask for enhanced user input.

- **Logo Theming:**  
  The logo changes for December (holiday) and June (pride) automatically.

---

## License

See the root repository for license details.  
Third-party libraries are included under their respective open-source licenses in the `wwwroot/lib/` directory.
