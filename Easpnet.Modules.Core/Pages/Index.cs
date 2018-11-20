using System;
using System.Collections.Generic;
using System.Text;

namespace Easpnet.Modules.Core.Pages
{
    public class Index : PageBase
    {
        public string TestString;

        protected void Page_Load(object sender, EventArgs e)
        {
            TestString = T("首页");
        }

    }
}
