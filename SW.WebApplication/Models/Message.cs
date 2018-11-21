using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SW.Models
{
    public class Message
    {
        private int id, owner;
        private string title, info, type, ip;
        private DateTime createTime;

        public int Id { get => id; set => id = value; }
        public int Owner { get => owner; set => owner = value; }
        public string Title { get => title; set => title = value; }
        public string Info { get => info; set => info = value; }
        public string Type { get => type; set => type = value; }
        public string Ip { get => ip; set => ip = value; }
        public DateTime CreateTime { get => createTime; set => createTime = value; }
    }
}