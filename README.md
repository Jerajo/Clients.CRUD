# Clients CRUD example (.NET Core + Vue.js)
Example project for the demonstration of a basic clients API with full CRUD access to the database and it's web client application on Vue.js.

## Project Setup
### Prerequisites
Follow the installation instructions for each dependency on their respective official web pages. (link over the dependency name)

[Windows compatible with .NET Core 3.1 runtime or any other compatible system](https://docs.microsoft.com/en-us/dotnet/core/install/windows?tabs=netcore31)

[.NET Core 3.1 LTS (SDK and Runtime)](https://dotnet.microsoft.com/download/dotnet-core/3.1)

[SQL Server 2019 (Developer or Express)](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

[Node package manager (npm == lts~v14.15.4)](https://nodejs.org/es/download/package-manager/)

### Manual development environment setup
In the root directory of the repository. Open a command prompt of your choice and enter the following commands:

#### 1. _install ef and create the database_
```
  cd Source/
  dotnet tool install --global dotnet-ef
  dotnet build
  dotnet ef database update --project ./Clients.Persistence.SqlServer
  dotnet test
```

#### 2. _install node modules_
```
  cd Clients.Vuejs/
  npm install
  npm run test
```

If everything passes without crashes you are good to go, I mean run :-)

### Running the projects
To run the API and Vue.js projects, you will need to open a command prompt for each one. Go to the repository path, from there run:

#### 1. _on the first one_
```
  cd Source/
  dotnet run --project ./Clients.Api
```

#### 2. _on the second one_
```
  cd Source/Clients.Vuejs/
  npm run serve
```

## Project details
### General information
Here some more information about the standards and rules applied on this repository.

#### Architectures
- N-Layers
- MVC

#### Code principles
- SOLID
- DRY

#### Design patterns
- Command
- Factory
- Dependency Injection
- Singleton
- Repository
- Unit of Work

#### NuGet packages
- EntityFrameworkCore
- FluentValidations
- AutoMapper
- Serilog

#### Have a great time!
I hope you fine this material very educational and useful.
