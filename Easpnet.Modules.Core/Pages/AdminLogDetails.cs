using System;
using System.Collections.Generic;
using System.Text;

namespace Easpnet.Modules.Core.Pages
{
    public class AdminLogDetails : PageBase
    {
        protected global::Xacc.PropertyGrid pg1;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            try
            {
                int i = Convert.ToInt32(Request["LogId"]);
                Log log = LogReader.ReadOneLog(i);
                pg1.SelectedObject = log;
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }

        }
    }
}
