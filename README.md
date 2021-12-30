# FinanceTracker-Backend
Backend for Finance Tracker Application

This api is developed using ASP.NET Core 3.1 and is hosted on Azure App Service.

Following Azure Resources are being utilized:
1. Azure App Service Plan + Azure App Service - Host the API
2. Azure SQL Server + Azure SQL DB - Store data
3. Azure Keyvault - Store Secrets
4. Azure Application Insights - Logging

![Azure Resources](https://user-images.githubusercontent.com/29853549/147735030-55731a85-55de-47ca-84c3-fb2a3f60eb29.png)

We followed the DB first approach and performed the following steps:
1. Created a SQL DB project
2. Created a user in SQL DB called gh-runner by running following commands
  ```
  CREATE USER [gh-runner] WITH PASSWORD = 'password';
  alter role db_datareader add member [gh-runner];
  alter role db_datawriter add member [gh-runner];
  alter role db_executor add member [gh-runner];
  GRANT VIEW DEFINITION TO [gh-runner];
  GRANT ALTER TO [gh-runner];
  GRANT ALTER ANY DATABASE SCOPED CONFIGURATION to [gh-runner];
  GRANT REFERENCES TO [gh-runner];
  ```
4. Using GitHub actions - Build & Deploy the SQL DB
5. Created API Project
6. Using the following command, created models and dbContext: "Scaffold-DbContext "Server=tcp:{server_url},1433;Initial Catalog={db_name};Persist Security Info=False;User ID={user_id};Password={password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models"
7. Gave DB access to Azure App Service API by running following command:
```
create user [financetracker-api] from external provider;
alter role db_datareader add member [financetracker-api];
alter role db_datawriter add member [financetracker-api];
alter role db_executor add member [financetracker-api];
```
8. Then deployed the azure app service as a docker image to GitHub Container Registry using GitHub actions.

![image](https://user-images.githubusercontent.com/29853549/147735152-bb31a8ff-9ea1-4e37-bbde-5a6c8df26244.png)

The API project has following features:
1. APIs authenticated with Azure AD
2. Keyvault and SQL accessed by managed identity, thus no secrets exposed anywhere
