using System;
using System.Collections.Generic;
using System.Text;

namespace Easpnet.Modules.Member.Pages
{
    public class CheckCode : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CheckCodeValue"] = CodeTest.MakeCode(this.Page);
        }
    }
}
