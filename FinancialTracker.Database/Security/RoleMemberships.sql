
CREATE ROLE db_executor;
GO
GRANT EXECUTE TO db_executor;
GO

create user [financetracker-api] from external provider;
GO
alter role db_datareader add member [financetracker-api];
GO
alter role db_datawriter add member [financetracker-api];
GO
alter role db_executor add member [financetracker-api];