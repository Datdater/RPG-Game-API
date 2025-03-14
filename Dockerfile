# Use .NET 8.0 runtime for the base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Use .NET 8.0 SDK for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the solution and restore dependencies
COPY ["RPG-API.sln", "."]
COPY ["RPG-API/RPG-API.csproj", "RPG-API/"]
RUN dotnet restore "RPG-API/RPG-API.csproj"

# Copy the entire project and build it
COPY . .
WORKDIR "/src/RPG-API"
RUN dotnet build "RPG-API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "RPG-API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Use base image for the final runtime
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RPG-API.dll"]
