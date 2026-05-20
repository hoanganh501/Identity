# =========================
# BASE (runtime)
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# =========================
# BUILD
# =========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

zCOPY Api/Api.csproj Api/
COPY Application/Application.csproj Application/
COPY Domain/Domain.csproj Domain/
COPY Infrastructure/Infrastructure.csproj Infrastructure/

# Restore
RUN dotnet restore Api/Api.csproj

# Copy full source
COPY . .

# Build
WORKDIR /src/Api
RUN dotnet build Api.csproj -c $BUILD_CONFIGURATION -o /app/build


# =========================
# PUBLISH
# =========================
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish Api.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false


# =========================
# FINAL
# =========================
FROM base AS final
WORKDIR /app

# Optional but recommended
ENV ASPNETCORE_URLS=http://+:8080

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Api.dll"]