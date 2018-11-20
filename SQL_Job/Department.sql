CREATE TABLE [dbo].[Department]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [CompanyId] INT NOT NULL, 
    [Info] NVARCHAR(MAX) NULL, 
    CONSTRAINT [FK_Department_ToCompany] FOREIGN KEY ([CompanyId]) REFERENCES [Company]([Id])
)
