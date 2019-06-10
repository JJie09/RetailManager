CREATE TABLE [dbo].[Sale]
(
	[Id] NVARCHAR(128) NOT NULL PRIMARY KEY, 
    [CashierId] NVARCHAR(128) NOT NULL, 
    [SaleDate] DATETIME2 NOT NULL, 
    [SubTotal] MONEY NOT NULL, 
    [Tax] MONEY NOT NULL, 
    [Total] MONEY NOT NULL, 
    CONSTRAINT [FK_Sale_ToUser] FOREIGN KEY (CashierId) REFERENCES [User](Id)
)
