using System;
using System.Collections.Generic;
using System.Text;

namespace Easpnet.Modules.Forum.Pages
{
    public class ForumThreedAdd : PageBase
    {
        protected OperateResult res = new OperateResult();
        protected Forum.Models.Theed md;
        protected bool isedit = false;


        protected List<Forum.Models.Theed> lst;


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            md = new Easpnet.Modules.Forum.Models.Theed();
            if (!string.IsNullOrEmpty(Get("id")))
            {
                md.TheedId = TypeConvert.ToInt64(Get("id"));
                if (md.GetModel())
                {
                    isedit = true;
                }
            }
            
            //
            Query q = Query.NewQuery();
            q.Where("TheedId", Symbol.GreaterThan, Ralation.End, 1).OrderBy("TheedId", OrderType.DESC);
            lst = md.GetList<Forum.Models.Theed>(q);
            

            Handler();
        }

        void Handler()
        {
            if (Post("action") == "add")
            {
                md.AddTime = DateTime.Now;
                md.Body = Post("body");
                md.Title = Post("title");
                long id = md.Create();
                if (id > 0)
                {
                    res.AddMessage("添加成功");
                }
            }
            if (Post("action") == "update")
            {
                md.Body = Post("body");
                md.Title = Post("title");
                
                if (md.Update())
                {
                    res.AddMessage("更新成功");
                }
            }
            
        }
    }
}
