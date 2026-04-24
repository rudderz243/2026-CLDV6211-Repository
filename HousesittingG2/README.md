# House Sitting — Group 2

## Summary

An ASP.NET Core MVC web application for managing house-sitting bookings. It demonstrates how to prevent **double bookings** — a booking cannot be created if the same customer already has an overlapping date range — and includes search/filter functionality to query bookings by date or customer. The database has two linked tables: **Customers** and **Bookings**, connected by a foreign key.

---

## Setup & Running

### 1. Create the Database (SSMS)

1. Open **SQL Server Management Studio (SSMS)**.
2. Open `HousesittingG2.sql` from the root of this project folder.
3. Click **Execute** (or press `F5`) to create the `HousesittingG2` database and all tables.

### 2. Update the Connection String

Open `HousesittingG2/appsettings.json` and update the `Server` value to match your SQL Server instance name:

```json
"ConnectionStrings": {
    "conn": "Server=YOUR_SERVER_NAME;Database=HousesittingG2;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

> Replace `YOUR_SERVER_NAME` with your actual server name (e.g. `localhost`, `.\SQLEXPRESS`).

### 3. Run the Project

Open `HousesittingG2.slnx` in Visual Studio and press **F5**, or run from the terminal:

```bash
dotnet run --project HousesittingG2
```

---

## Key Concepts & Resources

- **SQL — Date Comparisons & Overlap Detection**
  - [W3Schools — SQL BETWEEN](https://www.w3schools.com/sql/sql_between.asp)
  - [W3Schools — SQL WHERE](https://www.w3schools.com/sql/sql_where.asp)
  - [W3Schools — SQL Foreign Key](https://www.w3schools.com/sql/sql_foreignkey.asp)
- **LINQ — Querying with Conditions**
  - [Microsoft Learn — LINQ Query Syntax](https://learn.microsoft.com/en-us/dotnet/csharp/linq/get-started/introduction-to-linq-queries)
- **Entity Framework Core**
  - [Microsoft Learn — EF Core Get Started](https://learn.microsoft.com/en-us/ef/core/get-started/overview/first-app)
- **ASP.NET Core MVC**
  - [Microsoft Learn — MVC Overview](https://learn.microsoft.com/en-us/aspnet/core/mvc/overview)

---

## License

Licensed under the **PolyForm Noncommercial License 1.0.0** — permitted for personal study and educational use only. Commercial use is not allowed. See [`LICENSE.md`](./LICENSE.md) for details.
