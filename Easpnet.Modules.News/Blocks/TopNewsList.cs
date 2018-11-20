using System;
using System.Collections.Generic;
using System.Text;

namespace Easpnet.Modules.News.Blocks
{
    public class TopNewsList : BlockBase
    {
        protected List<Models.Article> ArticleDataList;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Query query = Query.NewQuery();
            int count = TypeConvert.ToInt32(this.Attributes["ListCount"]);
            long categoryId = TypeConvert.ToInt32(this.Attributes["NewsCategory"]);
            count = count <= 0 ? 5 : count;
            query.Where("CategoryId", Symbol.EqualTo, Ralation.End, categoryId).OrderBy("AddTime", OrderType.DESC);
            ArticleDataList = new Models.Article().GetList<Models.Article>(query, count);
        }
    }
}
