![cover](https://user-images.githubusercontent.com/8531546/207455956-5f6dfab6-3e11-4847-ba16-b931338b7eb6.png)

# Shorting

![MIT License](https://img.shields.io/apm/l/atomic-design-ui.svg)

A Simple URL shortener made in **Blazor** powered **.NET 7**, with Orleans and UI component library MudBlazor.
It uses Postgres for database persistence, and it deployable fully with Docker Compose.

## Index layout
  ![index](https://user-images.githubusercontent.com/8531546/207456011-44e90f11-8126-41b5-97d0-faaf2d3040ac.jpg)

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


