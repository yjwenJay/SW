using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SW.Models
{
    public class Company
    {
        private int id;
        private string name, info;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Info { get => info; set => info = value; }
    }
}