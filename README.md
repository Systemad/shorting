![cover](https://raw.githubusercontent.com/Systemad/shorting/main/img/cover.png)

# Shorting

![MIT License](https://img.shields.io/apm/l/atomic-design-ui.svg)

A Simple URL shortener made in **Blazor** powered **.NET 7**, with Orleans and UI component library MudBlazor.
It uses Postgres for database persistence, and it deployable fully with Docker Compose.

## Index layout
  ![index](https://raw.githubusercontent.com/Systemad/shorting/main/img/index.png)

## Libraries

- [.NET 7](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
- [Blazor](https://dotnet.microsoft.com/en-us/apps/aspnet/web-apps/blazor)
- [Orleans](https://learn.microsoft.com/en-us/dotnet/orleans/)
- [MudBlazor](https://mudblazor.com/)

## Setup and Deployment
- Clone the repository
- Edit username, password and database name, and modify connectionString in Program.cs accordingly.

- **Docker Compose**
    - Project is fully deployable with Docker Compose
    - ```docker compose up``` 
    - Deployment to Azure should work with [Azure Container Apps](https://azure.microsoft.com/en-us/products/container-apps/)


