using System;
using System.Collections.Generic;
using System.Text;

namespace Easpnet.Modules.Core.Pages
{
    public class AdminModuleManager : PageBase
    {
        protected List<Models.Module> modules;
        protected void Page_Load(object sender, EventArgs e)
        {
            Models.Module m = new Easpnet.Modules.Models.Module();
            Query q = Query.NewQuery();
            q.OrderBy("ModuleName");
            modules = m.GetList<Models.Module>(q);
        }

    }
}
