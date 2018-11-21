using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SW.Models
{
    public class Role
    {
        private int id, level;
        private string name, info;

        public int Id { get => id; set => id = value; }
        public int Level { get => level; set => level = value; }
        public string Name { get => name; set => name = value; }
        public string Info { get => info; set => info = value; }
    }
}