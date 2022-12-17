FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["shorting/shorting.csproj", "shorting/"]
RUN dotnet restore "shorting/shorting.csproj"
COPY . .
WORKDIR "/src/shorting"
RUN dotnet build "shorting.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "shorting.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "shorting.dll"]
