using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using Easpnet.Modules;

namespace SW.Model
{
    public class Modules : Nodes
    {
        public bool IsSub { get; set; }
        public List<SubModules> SubModules { get; set; }
    }
    public class SubModules : Nodes
    {

    }
    public class Nodes
    {
       
        private string id, name, href;

        public string Id { get => id; set => id = value; }
        public string Name { get; set; }
        public string Class { get => name; set => name = value; }
        public string Href { get => href; set => href = value; }
        
    }
}

