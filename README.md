# SimpleVendingMachine
## Summary
This solution consists of the 3 projects below.
- SimpleVendingMachine.Api - The RESTful API built with ASP.NET Core Web API.
- SimpleVendingMachine.Web - The Single Page Application built with Blazor WebAssembly.
- SimpleVendingMachine.Models - The class library contains Data Transfer Object classes.

## Get Started
### Prerequisites
Before running the solution, please make sure the following prerequisites are met.
- .NET 6
- Visual Studio 2022
- SQL Server 2019 (locally installed, Docker container, or Cloud hosted)
- Internet Connection (as font-awesome is retrieved from a CDN)
### Setup
The solution uses a code-first approach. The seed data is located in the file below.

*SimpleVendingMachine.Api/Data/VendingMachineDbContext.cs*

Thus, we can use Entity Framework Core Tools to create the database migrations. Though I pushed the migrations to the repo, please feel free to delete them and create new ones as needed.

In order to add migrations and create the database, please add SQL Server connection string to the *SimpleVendingMachine.Api/appsettings.json* file like below.

```
  "ConnectionStrings": {
    "VendingMachineConnection": ""
  }
```

Afterwards, please run the migration commands to provision the database. The [Migrations Overview](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=vs) includes the detailed steps for both Visual Studio and EF Core command-line tools.

Once the database is provisioned, please open the solution with Visual Studio, making both  SimpleVendingMachine.Api and SimpleVendingMachine.Web startup projects (Multiple startup projects). The SimpleVendingMachine.Api should be the top startup project in the list.

Then, we can build and run the solution. A swagger page and the Blazor page will pop up.

## Known Issues
So far, the following issues have been observed.

- Some icons might not be rendered correctly.
- Some Blazor isolated css styles might not work.

It seems like these are glitches caused by Blazor. Cleaning Web browser cache should be able to address them.