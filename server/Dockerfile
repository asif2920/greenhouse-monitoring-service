# ===== BASE RUNTIME =====
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# ===== BUILD STAGE =====
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution file
COPY greenhouse-monitoring-service.sln ./

# Copy project file
COPY server/src/Greenhouse.Api/Greenhouse.Api.csproj ./server/src/Greenhouse.Api/

# Restore dependencies
RUN dotnet restore ./server/src/Greenhouse.Api/Greenhouse.Api.csproj

# Copy full server folder
COPY server/ ./server/

# Build + publish
WORKDIR /src/server/src/Greenhouse.Api
RUN dotnet publish -c Release -o /app/publish

# ===== FINAL IMAGE =====
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "Greenhouse.Api.dll"]
