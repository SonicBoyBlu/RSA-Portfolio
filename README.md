# Portfolio Monorepo

This repository contains multiple projects and demos across different technologies, including C#, Vue/Nuxt, React, and more.

## Project Structure

```
.vs/                       # Visual Studio workspace state
C#/                        # .NET/C# backend and API projects
  API.Finance/
  API.Global/
  API.v3/
  Blazor.Comms/
  Micro.Jobs/
  Micro.Signup/
  Portal.Admin/
  Portal.PM.Partner/
CSS-HTML/                  # HTML/CSS demos
  Solar System/
React/                     # React demos
  Stocks Portfolio/
Vue-Nuxt/                  # Vue/Nuxt demos
  Jobs Board/
  Shopping Demo/
```

## Getting Started

### C# Projects

1. Open the `.sln` file in [C#/API.Finance/API.sln](C#/API.Finance/API.sln) or other solution files in Visual Studio.
2. Restore NuGet packages and build the solution.
3. Run the desired project (e.g., API, Blazor, etc.).

### Vue/Nuxt Projects

#### Jobs Board

```sh
cd "Vue-Nuxt/Jobs Board"
npm install
npm run dev
```

#### Shopping Demo

```sh
cd "Vue-Nuxt/Shopping Demo"
npm install
npm run dev
```

### React Projects

```sh
cd "React/Stocks Portfolio"
npm install
npm start
```

## Scripts

- Each frontend project contains its own `package.json` with scripts for development, build, and testing.
- C# projects use standard .NET CLI commands (`dotnet build`, `dotnet run`, etc.).

## Contributing

1. Fork the repository.
2. Create a new branch for your feature or bugfix.
3. Make your changes and commit them.
4. Open a pull request.

## License

This repository contains multiple projects, each with its own license. Please refer to the respective project folders for license details.