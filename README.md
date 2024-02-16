# Task Manager

### Built With

* [.NET 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

# Installing .NET 6

- [Downloads](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Installation docs](https://docs.microsoft.com/dotnet/core/install/)
- [Install Visual studio](https://docs.microsoft.com/en-us/visualstudio/install/install-visual-studio?view=vs-2022)

# Installing SSMS
- [Installation docs]https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16

## Getting Started

- On this project i have used the clean architecture
- The solution has three projects
  - 1. Cards project that has the APIs for cards creation and login 
  - 2. Core project is a class library that has mainly the business logics and all entities
  - 3. Infrastructure project is the data layer that has data related logics

## Settings

- You need to add these settings to your appsettings.json

```
	 "LogPath": "C:\\Temp\\Cards.Logs-.log",

	 "ConnectionStrings": {
	   "Card": "Server=(local);Database=Task_Manager;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
	 },

	 "Token": {
	   "Key": "V3taYOXpgeJAZoLqESuBBaYOXpgeJAZoLqFeV3taYOXpgeJAZoLqESuBBaYOXpgeJAZoLqFeoJESuBB2MKXagwoJESuBB2MKXagw",
	   "Issuer": "http://localhost:7190"
	 }
```

## Contact

Erick Muthomi - erick.muthomi@outlook.com

Project Link: [Task Manager](https://github.com/MuthomiEric/Task_Manager)

<p align="right">(<a href="#top">back to top</a>)</p>