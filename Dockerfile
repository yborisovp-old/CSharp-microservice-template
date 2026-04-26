FROM mcr.microsoft.com/dotnet/aspnet:10.0-noble AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENV DOTNET_EnableDiagnostics=0

FROM mcr.microsoft.com/dotnet/sdk:10.0-noble AS build
ARG BUILD_CONFIGURATION=Release

# Restore cache-friendly layer
WORKDIR /src
COPY Directory.Build.props ./
COPY Directory.Packages.props ./
COPY global.json ./
COPY src/ServiceTemplate.Api/ServiceTemplate.Api.csproj src/ServiceTemplate.Api/
COPY src/ServiceTemplate.Application/ServiceTemplate.Application.csproj src/ServiceTemplate.Application/
COPY src/ServiceTemplate.Contracts/ServiceTemplate.Contracts.csproj src/ServiceTemplate.Contracts/
COPY src/ServiceTemplate.Domain/ServiceTemplate.Domain.csproj src/ServiceTemplate.Domain/
COPY src/ServiceTemplate.Infrastructure/ServiceTemplate.Infrastructure.csproj src/ServiceTemplate.Infrastructure/
RUN dotnet restore src/ServiceTemplate.Api/ServiceTemplate.Api.csproj

COPY . .

WORKDIR /src/src/ServiceTemplate.Api

# Build the project
RUN dotnet build "ServiceTemplate.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ServiceTemplate.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
USER $APP_UID
ENTRYPOINT ["dotnet", "ServiceTemplate.Api.dll"]
