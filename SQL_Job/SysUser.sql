CREATE TABLE [dbo].[SysUser]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [PsswordHash] VARCHAR(50) NOT NULL, 
    [Phone] VARCHAR(50) NOT NULL, 
    [Email] VARCHAR(50) NOT NULL, 
    [Account] VARCHAR(50) NOT NULL, 
    [CreateTime] DATE NOT NULL, 
    [ModifyTime] DATE NULL, 
    [NickName] NVARCHAR(50) NOT NULL, 
    [CompanyId] INT NOT NULL, 
    [DepartmentId] INT NOT NULL, 
    [EmployeeId] INT NOT NULL, 
    CONSTRAINT [FK_SysUser_ToCompany] FOREIGN KEY ([CompanyId]) REFERENCES [Company]([Id]), 
    CONSTRAINT [FK_SysUser_ToDepartment] FOREIGN KEY ([DepartmentId]) REFERENCES [Department]([Id]), 
    CONSTRAINT [FK_SysUser_ToEmployee] FOREIGN KEY ([EmployeeId]) REFERENCES [Employee]([Id])
)
