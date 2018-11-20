using System;
using System.Collections.Generic;
using System.Text;
using Easpnet.Modules.News.Models;

namespace Easpnet.Modules.News.Pages
{
    public class ShowArticle : PageBase
    {
        protected Article article;
        protected Category category;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            article = new Article();
            article.ArticleId = TypeConvert.ToInt64(Request.QueryString["article_id"]);
            article.GetModel();


        }

    }
}
