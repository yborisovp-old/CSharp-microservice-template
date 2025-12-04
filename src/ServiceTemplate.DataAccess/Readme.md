# Migration Guide

This guide explains how to create and manage database migrations for the DataAccess project.

## Prerequisites

1. Find the `Host` project in Solution Explorer. Select "Set as StartUp Project" from the context menu.
2. Open the Tools -> NuGet Package Manager -> Package Manager Console window.
3. In the `Default project` drop-down list at the top of the window, select `ServiceTemplate.DataAccess`.

## Create a New Migration

### Using Package Manager Console:
```
Add-Migration Initial -Context DatabaseContext
```

### Using .NET CLI:
```shell
dotnet ef migrations add InitialCreate --context DatabaseContext --output-dir Migrations/Templates --project ../ServiceTemplate.DataAccess --startup-project ../Host/
```

## Generate SQL Script from Migrations

### Using Package Manager Console:
```
Script-Migration -Context DatabaseContext -From InitialCreate -To addRouter
```

### Using .NET CLI:
```shell
dotnet ef migrations script --context DatabaseContext --project ../ServiceTemplate.DataAccess --startup-project ../Host/ -o ../ServiceTemplate.DataAccess/Migrations/SQL/Templates/InitialCreate.sql
```

## Update Database (Optional)

Apply pending migrations to the database:

```shell
dotnet ef database update --project ./src/ServiceTemplate.DataAccess --startup-project ./src/Host/
```

