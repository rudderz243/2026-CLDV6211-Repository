# CLDV6211 Class Repository

A collection of ASP.NET Core MVC examples built during the CLDV6211 module. Each project demonstrates a specific set of cloud and web development concepts — from basic SQL scaffolding through Azure Blob Storage, containerisation with Docker, and referential integrity constraints.

All projects target **.NET 10** and use **Entity Framework Core** with a local SQL Server instance.

---

## Project Index

| Date       | Project                   | Covers                               |
| ---------- | ------------------------- | ------------------------------------ |
| 2026-02-26 | CarRentalExample          | SQL, EF Core scaffolding, CRUD       |
| 2026-03-04 | LibraryExample            | SQL, relational design, CRUD         |
| 2026-03-06 | DockerExample             | Containerisation, Dockerfile, Docker |
| 2026-03-16 | BirdWatcher               | Azure Blob Storage, image upload     |
| 2026-04-01 | HouseSitting              | Double-booking prevention, search    |
| 2026-04-24 | DeleteConstraintExample   | FK delete constraints, cascade rules |

Each project has its own `README.md` with setup instructions, relevant documentation links, and a license summary.

---

## Getting Started

### Prerequisites

- [Visual Studio 2022+](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- [.NET 10 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (Express or Developer edition)
- [SSMS](https://learn.microsoft.com/en-us/ssms/download-sql-server-management-studio-ssms) (SQL Server Management Studio)

### General Setup

1. Open the `.sql` file for the chosen project in SSMS and execute it to create the database and tables.
2. Open the `.slnx` solution file in Visual Studio.
3. Update the connection string in `appsettings.json` to match your SQL Server instance name.
4. Press **F5** (or `dotnet run`) to run the project.

See each project's `README.md` for project-specific instructions.

---

## License

This repository is licensed under the **PolyForm Noncommercial License 1.0.0**.

- ✅ **Permitted** — personal study, educational use, research, non-commercial projects
- ❌ **Not permitted** — commercial use, selling the software or derivatives
- 📋 **Conditions** — any redistribution must include a copy of these licence terms

> This code is intended for educational purposes as part of the CLDV6211 module.  
> See [`LICENSE.md`](./LICENSE.md) for the full licence text.
