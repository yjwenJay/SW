using SW.Business;
using SW.Commons.Xml;
using SW.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace SW.Controllers
{
    public class ArticleController : Controller
    {
        ArcticlesBLL arcticlesBLL = new ArcticlesBLL();
        string RootUrl = ConfigurationManager.AppSettings["Root_URL"];
        string PicURL = ConfigurationManager.AppSettings["Pic_URL"];

        // GET: Article
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            ModulesBLL modulesBLL = new ModulesBLL();
            ViewData["moudule"] = modulesBLL.GetModules(1);
            return View();
        }
        public JsonResult GetLists(int limit, int page)
        {
            try
            {
                int ModuleId = Request.Form["ModuleId"].ToInt32();
                string Name = Request.Form["Name"];
                List<Article_SysUser_Employee_Module> article_SysUsers = arcticlesBLL.GetArticles(1, "", 1);
                for (int i=0;i<article_SysUsers.Count;i++)
                {
                    article_SysUsers[i].CreateTimes = article_SysUsers[i].CreateTime.ToString("yyyy-MM-dd HH:mm:ss:ffff");
                }

                Newtonsoft.Json.JsonConvert.SerializeObject(new { code = 200, msg = "OK", count = article_SysUsers.Count, data = article_SysUsers });
                return new JsonResult()
                {
                    Data = new
                    {
                        total = article_SysUsers.Count,
                        rows = article_SysUsers.Skip(page).Take(limit).ToList()
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpGet]
        public JsonResult SearchLists(int limit, int page, int ModuleId, string Name)
        {
            try
            {
                List<Article_SysUser_Employee_Module> article_SysUsers = arcticlesBLL.GetArticles(1, Name, 0);

                Newtonsoft.Json.JsonConvert.SerializeObject(new { code = 200, msg = "OK", count = article_SysUsers.Count, data = article_SysUsers });
                return new JsonResult()
                {
                    Data = new
                    {
                        total = article_SysUsers.Count,
                        rows = article_SysUsers.Skip(page).Take(limit).ToList()
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpPost]
        public JsonResult Deleted()
        {
            try
            {
                string articlesId = Request.Form["IDs"]; 

                bool flag = arcticlesBLL.DeletedArticles(articlesId);

                return new JsonResult()
                {
                    Data = new
                    {
                        code = 200,
                        msg = "OK",
                        flag = flag
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ActionResult Added(Article article)
        {
            try
            {
                article.Guid = Guid.NewGuid().ToString();
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase f = Request.Files["ArcFiles"];
                    string[] types = f.ContentType.Split('/');
                    if (f.ContentType.Contains("image"))
                    {
                        string url = PicURL + article.Guid + "." + types[1];
                        f.SaveAs(url);
                        article.Image = "~/PortalDemo/statics/images/" + article.Guid + "." + types[1];
                    }
                    else
                    {
                        article.Image = "~/PortalDemo/statics/images/b0ce3f3cde0c084b6d42321b2dcbc407.jpeg";
                    }
                }
                else
                {
                    article.Image = "~/PortalDemo/statics/images/b0ce3f3cde0c084b6d42321b2dcbc407.jpeg";
                }
                //article.Guid = Guid.NewGuid().ToString();
                article.Contents = XmlHelper.WriteXmlContents(RootUrl + article.Guid, HttpUtility.HtmlDecode(article.Contents));
                article.Owner = 1;
                article.CreateTime = DateTime.Now;//.ToString("yyyy-MM-dd HH:mm:ss:ffff")
                article.ModifyTime = DateTime.Now;//.ToString("yyyy-MM-dd HH:mm:ss:ffff")
                article.Modify = 1;
                article.ModifyInfo = string.Empty;
                article.Type = article.ModuleId;
                article.Click = 0;
 
                article.Info = article.Info.Substring(0,30)+"......";
                article.Link = article.Guid;
                bool flag = arcticlesBLL.AddedArticles(article);
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}