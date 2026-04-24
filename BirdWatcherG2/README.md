# Bird Watcher — Group 2

## Summary

An ASP.NET Core MVC web application for logging bird sightings. Users can record a description, timestamp, and upload an image of each spotting. This project introduces **Azure Blob Storage** — images are stored in the cloud rather than on disk, with the `ImageURL` column in the database pointing to the blob. It uses Entity Framework Core for database access and the Azure Storage SDK for blob operations.

---

## Setup & Running

### 1. Create the Database (SSMS)

1. Open **SQL Server Management Studio (SSMS)**.
2. Open `BirdWatcherG2.sql` from the root of this project folder.
3. Click **Execute** (or press `F5`) to create the `BirdWatcherG2` database and the `Spottings` table.

### 2. Update the Connection String

Open `BirdWatcherG2/appsettings.json` and update both connection strings:

```json
"ConnectionStrings": {
    "birdy": "Server=YOUR_SERVER_NAME;Database=BirdWatcherG2;Trusted_Connection=True;TrustServerCertificate=True;",
    "storage": "UseDevelopmentStorage=True;"
}
```

- Replace `YOUR_SERVER_NAME` with your SQL Server instance name (e.g. `localhost`, `.\SQLEXPRESS`).
- `"UseDevelopmentStorage=True"` uses [Azurite](https://learn.microsoft.com/en-us/azure/storage/common/storage-use-azurite) (local Azure Storage emulator). Replace with a real Azure connection string when deploying to the cloud.

### 3. Start Azurite (Local Blob Emulator)

If you don't have an Azure account, use the Azurite emulator:

```bash
# Install globally (once)
npm install -g azurite

# Start the emulator
azurite
```

Or use the **Azurite** extension directly inside VS Code.

### 4. Run the Project

Open `BirdWatcherG2.slnx` in Visual Studio and press **F5**, or run from the terminal:

```bash
dotnet run --project BirdWatcherG2
```

---

## Key Concepts & Resources

- **Azure Blob Storage**
  - [Microsoft Learn — Azure Blob Storage Introduction](https://learn.microsoft.com/en-us/azure/storage/blobs/storage-blobs-introduction)
  - [Microsoft Learn — Upload Blob with .NET](https://learn.microsoft.com/en-us/azure/storage/blobs/storage-blob-upload)
  - [Microsoft Learn — Azurite Emulator](https://learn.microsoft.com/en-us/azure/storage/common/storage-use-azurite)
- **SQL**
  - [W3Schools — SQL CREATE TABLE](https://www.w3schools.com/sql/sql_create_table.asp)
- **Entity Framework Core**
  - [Microsoft Learn — EF Core Get Started](https://learn.microsoft.com/en-us/ef/core/get-started/overview/first-app)

---

## License

Licensed under the **PolyForm Noncommercial License 1.0.0** — permitted for personal study and educational use only. Commercial use is not allowed. See [`LICENSE.md`](./LICENSE.md) for details.
