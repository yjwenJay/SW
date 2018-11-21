using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SW.Models
{
    public class Authorization
    {
        private int id, roleId, moduleId;
        private string author;

        public int Id { get => id; set => id = value; }
        public int RoleId { get => roleId; set => roleId = value; }
        public int ModuleId { get => moduleId; set => moduleId = value; }
        public string Author { get => author; set => author = value; }
    }
}