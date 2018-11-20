using System;
using System.Collections.Generic;
using System.Text;

namespace Easpnet.Modules.Core.Pages
{
    public class AdminBlockManager : PageBase
    {
        protected List<Models.BlockInfo> blocks;
        protected void Page_Load(object sender, EventArgs e)
        {
            //
            Models.BlockInfo m = new Easpnet.Modules.Models.BlockInfo();
            Query q = Query.NewQuery();
            q.OrderBy("BlockName");
            blocks = m.GetList<Models.BlockInfo>(q);
        }
    }
}
