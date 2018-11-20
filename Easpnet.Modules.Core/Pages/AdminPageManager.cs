using System;
using System.Collections.Generic;
using System.Text;
using Easpnet.Modules.Models;

namespace Easpnet.Modules.Core.Pages
{
    public class AdminPageManager : PageBase
    {
        protected List<Models.PageInfo> modules;
        protected void Page_Load(object sender, EventArgs e)
        {
            //
            Hander();

            //
            Models.PageInfo m = new Easpnet.Modules.Models.PageInfo();
            Query q = Query.NewQuery();
            if (!string.IsNullOrEmpty(Get("pageName")))
            {
                q.Where("PageName", Symbol.EqualTo, Ralation.And, Get("pageName"));
            }
            q.Where("1", Symbol.EqualTo, Ralation.End, 1);
            q.OrderBy("PageName");
            modules = m.GetList<Models.PageInfo>(q);
        }

        void Hander()
        {
            switch (Get("action"))
            {                
                default:
                    break;
            }
        }


    }
}
