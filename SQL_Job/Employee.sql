CREATE TABLE [dbo].[Employee]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [CompanyId] INT NOT NULL, 
    [DepartmentId] INT NOT NULL, 
    CONSTRAINT [FK_Employee_ToCompany] FOREIGN KEY ([CompanyId]) REFERENCES [Company]([Id]), 
    CONSTRAINT [FK_Employee_ToDepartment] FOREIGN KEY ([DepartmentId]) REFERENCES [Department]([Id])
)
