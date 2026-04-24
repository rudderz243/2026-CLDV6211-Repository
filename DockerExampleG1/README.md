# Docker Example — Group 1

## Summary

An ASP.NET Core MVC web application demonstrating how to containerise a .NET web app using **Docker**. The project includes a `Dockerfile` that packages the application into a self-contained image, ready to be built and run in any Docker-compatible environment. This example focuses on the containerisation workflow — no SQL database is required.

---

## Setup & Running

### Option A — Run Directly (Visual Studio)

1. Open `DockerExampleG1.slnx` in Visual Studio.
2. Select **Docker** as the launch profile from the dropdown next to the run button.
3. Press **F5** — Visual Studio will build the image and start the container automatically.

### Option B — Run via Docker CLI

From inside the `DockerExampleG1/` folder (where the `Dockerfile` lives):

```bash
# Build the image
docker build -t dockerexampleg1 .

# Run the container
docker run -p 8080:8080 dockerexampleg1
```

Then open your browser at `http://localhost:8080`.

### Option C — Run Without Docker

Open `DockerExampleG1.slnx` in Visual Studio and press **F5** using the `http` profile, or run:

```bash
dotnet run --project DockerExampleG1
```

---

## Key Concepts & Resources

- **Docker & Containerisation**
  - [Microsoft Learn — Containerise a .NET App](https://learn.microsoft.com/en-us/dotnet/core/docker/build-container)
  - [Docker Docs — Dockerfile Reference](https://docs.docker.com/reference/dockerfile/)
- **ASP.NET Core MVC**
  - [Microsoft Learn — MVC Overview](https://learn.microsoft.com/en-us/aspnet/core/mvc/overview)
- **Docker Hub — .NET Images**
  - [mcr.microsoft.com/dotnet](https://hub.docker.com/_/microsoft-dotnet)

---

## License

Licensed under the **PolyForm Noncommercial License 1.0.0** — permitted for personal study and educational use only. Commercial use is not allowed. See [`LICENSE.md`](./LICENSE.md) for details.
