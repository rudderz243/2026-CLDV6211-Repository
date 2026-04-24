# Delete Constraint Example — Group 1

## Summary

An ASP.NET Core MVC web application demonstrating how **foreign key delete constraints** work in SQL Server. The database models a student–module registration system with three tables: **Modules**, **Students**, and **ModuleStudents** (the junction table). The project explores what happens when you attempt to delete a parent record that is still referenced by child rows — and how to configure `ON DELETE CASCADE` or `ON DELETE NO ACTION` to control that behaviour.

---

## Setup & Running

### 1. Create the Database (SSMS)

1. Open **SQL Server Management Studio (SSMS)**.
2. Open `StudentG1.sql` from the root of this project folder.
3. Click **Execute** (or press `F5`) to create the `StudentG1` database and all tables.

### 2. Update the Connection String

Open `DeleteConstraintExampleG1/appsettings.json` and update the `Server` value to match your SQL Server instance name:

```json
"ConnectionStrings": {
    "conn": "Server=YOUR_SERVER_NAME;Database=StudentG1;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

> Replace `YOUR_SERVER_NAME` with your actual server name (e.g. `localhost`, `.\SQLEXPRESS`).

### 3. Run the Project

Open `DeleteConstraintExampleG1.slnx` in Visual Studio and press **F5**, or run from the terminal:

```bash
dotnet run --project DeleteConstraintExampleG1
```

---

## Key Concepts & Resources

- **SQL — Foreign Key Constraints & Delete Behaviour**
  - [W3Schools — SQL Foreign Key](https://www.w3schools.com/sql/sql_foreignkey.asp)
  - [W3Schools — SQL Constraints](https://www.w3schools.com/sql/sql_constraints.asp)
  - [Microsoft Learn — ON DELETE Clause](https://learn.microsoft.com/en-us/sql/t-sql/statements/create-table-transact-sql#on-delete)
- **Entity Framework Core — Cascade Delete**
  - [Microsoft Learn — Cascade Delete](https://learn.microsoft.com/en-us/ef/core/saving/cascade-delete)
- **ASP.NET Core MVC**
  - [Microsoft Learn — MVC Overview](https://learn.microsoft.com/en-us/aspnet/core/mvc/overview)

---

## License

Licensed under the **PolyForm Noncommercial License 1.0.0** — permitted for personal study and educational use only. Commercial use is not allowed. See [`LICENSE.md`](./LICENSE.md) for details.
