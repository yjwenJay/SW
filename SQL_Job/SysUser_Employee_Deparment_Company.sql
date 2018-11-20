USE [SW]
GO

/****** Object:  View [dbo].[SysUser_Employee_Deparment_Company]    Script Date: 08/30/2018 13:31:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[SysUser_Employee_Deparment_Company] AS 
SELECT DISTINCT
dbo.SysUser.Id,
dbo.SysUser.PsswordHash,
dbo.SysUser.Phone,
dbo.SysUser.Email,
dbo.SysUser.Account,
dbo.SysUser.CreateTime,
dbo.SysUser.ModifyTime,
dbo.SysUser.NickName,
dbo.SysUser.CompanyId,
dbo.SysUser.DepartmentId,
dbo.SysUser.EmployeeId,
dbo.Employee.Name AS EmployeeName,
dbo.Department.Name AS DepartmentName,
dbo.Department.Info AS DepartmentInfo,
dbo.Company.Name AS CompanyName,
dbo.Company.Info AS CompanyInfo

FROM
dbo.SysUser
INNER JOIN dbo.Employee ON dbo.SysUser.EmployeeId = dbo.Employee.Id
INNER JOIN dbo.Department ON dbo.SysUser.DepartmentId = dbo.Department.Id AND dbo.Employee.DepartmentId = dbo.Department.Id AND '' = ''
INNER JOIN dbo.Company ON dbo.SysUser.CompanyId = dbo.Company.Id AND dbo.Employee.CompanyId = dbo.Company.Id AND dbo.Department.CompanyId = dbo.Company.Id

order by SysUser.Id
GO


