USE [SW]
GO

/****** Object:  View [dbo].[SysUser_Role_Module_Authorization]    Script Date: 08/30/2018 13:45:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[SysUser_Role_Module_Authorization] AS 
SELECT
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
roles.Name,
roles.Info,
roles.[Level],
module.Name as ModuleName,
module.FatherModule,
module.Image,
module.Level as ModuleLevel,
module.Link,
author.Id AS AuthorId,
author.Author,
author.RoleId,
author.ModuleId

FROM
dbo.SysUser
INNER JOIN dbo.Role as roles ON dbo.SysUser.RoleId = roles.Id
INNER JOIN dbo.[Authorization] as author ON author.RoleId = roles.Id
inner join dbo.Module as module on Module.Id = author.ModuleId
GO


