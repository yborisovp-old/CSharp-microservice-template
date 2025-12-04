# Migration update
1. Find the `Host` project in Solution Explorer. Select Set as StartUp Project from the context menu.
2. Open the Tools -> NuGet Package Manager -> Package Manager Console window.
3. In the `Default project` drop-down list at the top of the window, select `ServiceTemplate.DataAccess`.

## Create a new migration using the command:

```
Add-Migration Initial -Context DataBaseContext
```
Or
```shell
dotnet ef migrations add InitialCreate --context DatabaseContext  --output-dir Migrations/Tempaltes  --project ../ServiceTemplate.DataAccess --startup-project ../Host/
```

## Create an SQL script using the command:
```
Script-Migration -Context DataBaseContext -From InitialCreate -To addRouter
```

Or

```shell
 dotnet ef migrations script --context DatabaseContext --project ../ServiceTemplate.DataAccess --startup-project ../Host/ -o ../ServiceTemplate.DataAccess/Migrations/SQL/Tempaltes/InitialCreate.sql
```

## [Optional] Update the database with the command
```shell
dotnet ef database update --project ./src/ServiceTemplate.DataAccess --startup-project ./src/Host/
```

