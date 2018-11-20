using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SW.Models
{
    public class Moudule
    {
        private int id, fatherMoudule, level;
        private string name, image, link;

        public int Id { get => id; set => id = value; }
        public int FatherMoudule { get => fatherMoudule; set => fatherMoudule = value; }
        public int Level { get => level; set => level = value; }
        public string Name { get => name; set => name = value; }
        public string Image { get => image; set => image = value; }
        public string Link { get => link; set => link = value; }
    }
}