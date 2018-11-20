CREATE TABLE [dbo].[Module]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NULL, 
    [FatherModule] INT NULL, 
    [Level] INT NULL, 
    [Image] NVARCHAR(50) NULL, 
    [Link] VARCHAR(50) NULL
)
