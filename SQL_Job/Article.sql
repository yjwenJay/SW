CREATE TABLE [dbo].[Article]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Title] VARCHAR(50) NOT NULL, 
    [Info] NVARCHAR(MAX) NOT NULL, 
    [Owner] INT NOT NULL, 
    [CreateTime] DATE NOT NULL, 
    [ModifyTime] DATE NULL, 
    [Modify] INT NULL, 
    [ModifyInfo] NVARCHAR(50) NULL, 
    [Type] NVARCHAR(50) NOT NULL, 
    [Click] INT NULL, 
    CONSTRAINT [FK_Article_ToOwnerEmployee] FOREIGN KEY ([Owner]) REFERENCES [Employee]([Id]), 
    CONSTRAINT [FK_Article_ToModifyEmployee] FOREIGN KEY ([Modify]) REFERENCES [Employee]([Id])
)
