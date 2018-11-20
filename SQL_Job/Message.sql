CREATE TABLE [dbo].[Message]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Owner] INT NULL, 
    [Title] NVARCHAR(50) NOT NULL, 
    [Info] NVARCHAR(MAX) NOT NULL, 
    [CreateTime] DATE NOT NULL, 
    [Type] NVARCHAR(50) NULL, 
    [IP] NVARCHAR(50) NULL, 
    CONSTRAINT [FK_Message_ToEmployee] FOREIGN KEY ([Owner]) REFERENCES [Employee]([Id])
)
