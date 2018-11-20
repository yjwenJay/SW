using Easpnet;
using SW.Commons.Xml;
using SW.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW.Business
{
    public class ArcticlesBLL
    {
        /// <summary>
        /// 获取文章list
        /// </summary>
        /// <param name="_owner">权限</param>
        /// <returns></returns>
        public List<Article> GetArticles(int _owner = 1)
        {
            try
            {
                Db db = new Db("Article");
                if (_owner > 0)//权限获取
                {
                    db.Where("Owner", _owner);
                    return db.Select<Article>();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 获取文章list
        /// </summary>
        /// <param name="_owner">权限</param>
        /// <param name="userName">归属人名</param>
        /// <param name="moduleId">模块ID</param>
        /// <returns></returns>
        public List<Article_SysUser_Employee_Module> GetArticles(int _owner = 1, string userName = "", int moduleId = 1)
        {
            try
            {
                List<Article_SysUser_Employee_Module> article_SysUsers = new List<Article_SysUser_Employee_Module>();
                Db db = new Db("Article_SysUser_Employee_Module");

                if (!String.IsNullOrEmpty(userName))//用户名
                {
                    db.Where("Name", "like", "%" + userName + "%");
                }
                if (moduleId > 0)//模块ID
                {
                    db.Where("ModuleId", moduleId);
                }
                if (_owner > 0)//权限获取
                {
                    db.Where("UserId", _owner);
                }
                else
                {
                    return null;
                }
                article_SysUsers = db.Select<Article_SysUser_Employee_Module>();
                if (article_SysUsers.Count > 0)
                {
                    foreach (var items in article_SysUsers)
                    {
                        items.Contents = XmlHelper.GetXmlContents(items.Contents);
                    } 
                }
                return article_SysUsers;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool DeletedArticles(string _articles)
        {
            try
            {
                Article article = new Article();
                Query query = Query.NewQuery();

                query.Where("Guid", Symbol.EqualTo, Ralation.And, _articles);

                return article.Delete(query) > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool AddedArticles(Article _articles)
        {
            try
            {
                Article article = new Article();
                article = _articles;
                return article.Create() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
