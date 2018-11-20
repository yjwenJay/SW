using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SW.Models
{
    public class Employee
    {
        private int id, companyId, departmentId;
        private string name;

        public int Id { get => id; set => id = value; }
        public int CompanyId { get => companyId; set => companyId = value; }
        public int DepartmentId { get => departmentId; set => departmentId = value; }
        public string Name { get => name; set => name = value; }
    }
}