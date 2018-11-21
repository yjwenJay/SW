using SW.Business;
using SW.Commons.Xml;
using SW.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SW.WebApplication.Controllers
{
    public class PortalController : Controller
    {
        // GET: Portal
        public ActionResult Index()
        {
            ViewData["meunArticles"] = GetArticles();
            ViewData["meunItems"] = GetMeunItems();
            return View();
        }
        public ActionResult Layout()
        {     
            ViewData["meunItems"] = GetMeunItems();
            return View();
        }

        public ActionResult Details()
        {
            ViewData["meunItems"] = GetMeunItems();
            return View();
        }

        private List<Modules>  GetMeunItems()
        {
            Random rd = new Random();
            List<Modules> modules = new List<Modules>();
            List<SubModules> subModules = new List<SubModules>();
            int count = 0;
            for (int i = 0; i < 6; i++)
            {
                if (count == 0)
                {
                    Modules module0 = new Modules();
                    string id = CONSTSTATICENUM.LAYOUTMEUNITEMID + rd.Next(0, 999);
                    module0.Id = id;
                    module0.Class = CONSTSTATICENUM.LAYOUTMEUNITEMFCLASS + id;
                    module0.Href = "/AdminCMS/Index";
                    module0.IsSub = false;
                    module0.Name = "后台AdminCMS";
                    modules.Add(module0);
                    count++;
                }
                else if (count < 3)
                {
                    Modules module1 = new Modules();
                    string id = CONSTSTATICENUM.LAYOUTMEUNITEMID + rd.Next(0, 999);
                    module1.Id = id;
                    module1.Class = CONSTSTATICENUM.LAYOUTMEUNITEMNCLASS + id;
                    module1.Href = "#";
                    module1.IsSub = false;
                    module1.Name = "module1";
                    modules.Add(module1);
                    count++;
                }
                else if (count >= 3 && count < 5)
                {
                    Modules module2 = new Modules();
                    string id = CONSTSTATICENUM.LAYOUTMEUNITEMID + rd.Next(0, 999);
                    module2.Id = id;
                    module2.Name = "module2";
                    module2.IsSub = true;
                    module2.Class = CONSTSTATICENUM.LAYOUTMEUNITEMSCLASS + id;
                    module2.Href = "#";

                    SubModules module3 = new SubModules();
                    string id2 = CONSTSTATICENUM.LAYOUTMEUNITEMID + rd.Next(0, 999);
                    module3.Id = id2;
                    module3.Name = "module3";
                    module3.Class = CONSTSTATICENUM.LAYOUTMEUNITEMSSCLASS + id2;
                    module3.Href = "#";
                    subModules.Add(module3);
                    module2.SubModules = subModules;
                    modules.Add(module2);
                    count++;
                }
            }
            return modules;
        }
        private List<Article> GetArticles()
        {
            ArcticlesBLL article = new ArcticlesBLL();
            List<Article> articles = article.GetArticles(1);
            for (int i=0;i<articles.Count;i++)
            {
                articles[i].Image = CONSTSTATICENUM.PORTALIMGAGELINK + articles[i].Image;
                articles[i].Comments = i;
                articles[i].Contents = XmlHelper.GetXmlContents(articles[i].Contents);
            }

            return articles;
        }
    }
}