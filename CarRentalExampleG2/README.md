# Car Rental Example — Group 2

## Summary

An ASP.NET Core MVC web application that manages a car rental system. It demonstrates basic SQL database design and Entity Framework Core scaffolding for full CRUD operations across three related tables: **Cars**, **Customers**, and **Bookings**. Foreign key relationships use `ON DELETE CASCADE` so removing a car or customer automatically clears associated bookings.

---

## Setup & Running

### 1. Create the Database (SSMS)

1. Open **SQL Server Management Studio (SSMS)**.
2. Open the file `CarRentalExampleG2.sql` located in the root of this project folder.
3. Click **Execute** (or press `F5`) to create the database and all tables.

### 2. Update the Connection String

Open `CarRentalG2/appsettings.json` and update the `Server` value to match your SQL Server instance name:

```json
"ConnectionStrings": {
    "ConnString": "Server=YOUR_SERVER_NAME;Database=CarRentalExampleG2;Trusted_Connection=true;TrustServerCertificate=true;"
}
```

> Replace `YOUR_SERVER_NAME` with your actual server name (e.g. `localhost`, `.\SQLEXPRESS`).

### 3. Run the Project

Open `CarRentalG2.slnx` in Visual Studio and press **F5**, or run from the terminal:

```bash
dotnet run --project CarRentalG2
```

---

## Key Concepts & Resources

- **SQL — CREATE TABLE, PRIMARY KEY, FOREIGN KEY**
  - [W3Schools — SQL CREATE TABLE](https://www.w3schools.com/sql/sql_create_table.asp)
  - [W3Schools — SQL Foreign Key](https://www.w3schools.com/sql/sql_foreignkey.asp)
- **Entity Framework Core — Scaffolding & DbContext**
  - [Microsoft Learn — EF Core Get Started](https://learn.microsoft.com/en-us/ef/core/get-started/overview/first-app)
  - [Microsoft Learn — Database-First Scaffolding](https://learn.microsoft.com/en-us/ef/core/managing-schemas/scaffolding/)
- **ASP.NET Core MVC**
  - [Microsoft Learn — MVC Overview](https://learn.microsoft.com/en-us/aspnet/core/mvc/overview)

---

## License

Licensed under the **PolyForm Noncommercial License 1.0.0** — permitted for personal study and educational use only. Commercial use is not allowed. See [`LICENSE.md`](./LICENSE.md) for details.
