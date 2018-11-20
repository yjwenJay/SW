using System;
using System.Collections.Generic;
using System.Text;

namespace Easpnet.Modules.Core.Pages
{
    public class AdminPageGroupManager : PageBase
    {
        protected List<Models.PageGroup> list;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //
            Models.PageGroup m = new Easpnet.Modules.Models.PageGroup();
            Query q = Query.NewQuery();
            q.OrderBy("PageGroupName");
            list = m.GetList<Models.PageGroup>(q);
        }
    }
}
