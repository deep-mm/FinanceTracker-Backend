# FinanceTracker-Backend
Backend for Finance Tracker Application

This api is developed using ASP.NET Core 3.1 and is hosted on Azure App Service.

Following Azure Resources are being utilized:
1. Azure App Service Plan + Azure App Service - Host the API
2. Azure SQL Server + Azure SQL DB - Store data
3. Azure Keyvault - Store Secrets
4. Azure Application Insights - Logging

We followed the DB first approach and performed the following steps:
1. Created a SQL DB project
2. Created a user in SQL DB called gh-runner by running following commands
2. Using GitHub actions - Build & Deploy the SQL DB
3. Created API Project
4. Using the following command, created models and dbContext: "Scaffold-DbContext "Server=tcp:{server_url},1433;Initial Catalog={db_name};Persist Security Info=False;User ID={user_id};Password={password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models"
5. 
