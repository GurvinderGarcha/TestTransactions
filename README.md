# TestTransactions

Please update the connection string in the web config file. And to create new database and run the  following query against that database.

Create database TransactionData
(
  Id int primary key identity(1,1),
  Account nvarchar(100) not null,
  Description nvarchar(1000) not null,
  CurrencyCode nvarchar(20) not null,
  Amount decimal(10,2) not null
)
