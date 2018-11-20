using System;
using System.Collections.Generic;
using System.Text;

namespace Easpnet.Modules.Core.Pages
{
    public class Info : PageBase
    {
        public string Message;


        protected void Page_Load(object sender, EventArgs e)
        {
            Message = Request.QueryString["msg"];

        }
    }
}
