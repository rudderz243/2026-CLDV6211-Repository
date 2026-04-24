# Library Example — Group 1

## Summary

An ASP.NET Core MVC web application that models a public library system. It covers relational database design with three linked tables — **Books**, **Patrons**, and **Loans** — and uses Entity Framework Core to scaffold full CRUD operations. The project reinforces foreign key relationships and how to design a many-to-many junction through a dedicated `Loans` table.

---

## Setup & Running

### 1. Create the Database (SSMS)

1. Open **SQL Server Management Studio (SSMS)**.
2. Open the file `LibraryG1.sql` located in the root of this project folder.
3. Click **Execute** (or press `F5`) to create the `LibraryDBG1` database and all tables.

> **Note:** The SQL file comments out the `CREATE DATABASE` statement — you may need to create the database manually in SSMS first (right-click **Databases → New Database** and name it `LibraryDBG1`), then run the rest of the script.

### 2. Update the Connection String

Open `LibraryExampleG1/appsettings.json` and update the `Server` value to match your SQL Server instance name:

```json
"ConnectionStrings": {
    "ConnString": "Server=YOUR_SERVER_NAME;Database=LibraryDBG1;Trusted_Connection=true;TrustServerCertificate=true;"
}
```

> Replace `YOUR_SERVER_NAME` with your actual server name (e.g. `localhost`, `.\SQLEXPRESS`).

### 3. Run the Project

Open `LibraryExampleG1.slnx` in Visual Studio and press **F5**, or run from the terminal:

```bash
dotnet run --project LibraryExampleG1
```

---

## Key Concepts & Resources

- **SQL — Relational Design & Joins**
  - [W3Schools — SQL CREATE TABLE](https://www.w3schools.com/sql/sql_create_table.asp)
  - [W3Schools — SQL JOIN](https://www.w3schools.com/sql/sql_join.asp)
  - [W3Schools — SQL Foreign Key](https://www.w3schools.com/sql/sql_foreignkey.asp)
- **Entity Framework Core**
  - [Microsoft Learn — EF Core Get Started](https://learn.microsoft.com/en-us/ef/core/get-started/overview/first-app)
  - [Microsoft Learn — Relationships](https://learn.microsoft.com/en-us/ef/core/modeling/relationships)
- **ASP.NET Core MVC**
  - [Microsoft Learn — MVC Overview](https://learn.microsoft.com/en-us/aspnet/core/mvc/overview)

---

## License

Licensed under the **PolyForm Noncommercial License 1.0.0** — permitted for personal study and educational use only. Commercial use is not allowed. See [`LICENSE.md`](./LICENSE.md) for details.
