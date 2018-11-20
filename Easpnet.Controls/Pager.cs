using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Easpnet.Controls
{
    public class Pager : Wuqi.Webdiyer.AspNetPager
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            EnableUrlRewriting = true;
            UrlRewritePattern = Page.Request.RawUrl;
            UrlRewritePattern = this.Page.Server.UrlDecode(UrlRewritePattern);
            if (Regex.Match(UrlRewritePattern, UrlPageIndexName + "=\\d+").Success)
            {
                UrlRewritePattern = Regex.Replace(UrlRewritePattern, UrlPageIndexName + "=\\d+", "pageindex={0}");
            }
            else
            {
                if (UrlRewritePattern.IndexOf("?") > -1)
                {
                    UrlRewritePattern += "&pageindex={0}";
                }
                else
                {
                    UrlRewritePattern += "?pageindex={0}";
                }
            }

            //
            int index = 1;
            int.TryParse(Page.Request.QueryString[UrlPageIndexName], out index);
            CurrentPageIndex = index;
        }


        
    }
}
