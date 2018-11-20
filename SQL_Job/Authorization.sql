CREATE TABLE [dbo].[Authorization]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Author] VARCHAR(4) NOT NULL, 
    [RoleId] INT NOT NULL, 
    [ModuleId] INT NOT NULL, 
    CONSTRAINT [FK_Authorization_ToRole] FOREIGN KEY ([RoleId]) REFERENCES [Role]([Id]), 
    CONSTRAINT [FK_Authorization_ToMoudule] FOREIGN KEY ([ModuleId]) REFERENCES [Module]([Id])
)
