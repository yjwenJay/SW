using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SW.Models
{
    public class SysUser
    {
        private int id, companyId, departmentId, employeeId;
        private string psswordHash, phone, email, account, nickName;
        private DateTime createTime, codifyTime;

        public int Id { get => id; set => id = value; }
        public int CompanyId { get => companyId; set => companyId = value; }
        public int DepartmentId { get => departmentId; set => departmentId = value; }
        public int EmployeeId { get => employeeId; set => employeeId = value; }
        public string PsswordHash { get => psswordHash; set => psswordHash = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Email { get => email; set => email = value; }
        public string Account { get => account; set => account = value; }
        public string NickName { get => nickName; set => nickName = value; }
        public DateTime CreateTime { get => createTime; set => createTime = value; }
        public DateTime CodifyTime { get => codifyTime; set => codifyTime = value; }
    }
}