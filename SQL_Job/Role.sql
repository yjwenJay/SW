CREATE TABLE [dbo].[Role]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Info] NVARCHAR(50) NULL, 
    [Level] INT NOT NULL
)
