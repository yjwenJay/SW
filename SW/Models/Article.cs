using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SW.Models
{
    public class Article
    {
        private int id,owner,modify,click;
        private string title, info, modifyInfo, type, createTime, codifyTime;

        public int Id { get => id; set => id = value; }
        public int Owner { get => owner; set => owner = value; }
        public int Modify { get => modify; set => modify = value; }
        public int Click { get => click; set => click = value; }
        public string Title { get => title; set => title = value; }
        public string Info { get => info; set => info = value; }
        public string ModifyInfo { get => modifyInfo; set => modifyInfo = value; }
        public string Type { get => type; set => type = value; }
        public string CreateTime { get => createTime; set => createTime = value; }
        public string CodifyTime { get => codifyTime; set => codifyTime = value; }
    }
}