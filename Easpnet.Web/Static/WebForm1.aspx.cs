using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Easpnet.Modules.Models;
using Easpnet.Modules;

namespace Easpnet.Web.Static
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Procedure proc = new Procedure("test_proc");
            //proc.AddInParameter("RealName", System.Data.DbType.String, 255, "秦东");
            //proc.AddInParameter("AgentId", System.Data.DbType.Int64, 0, 5);
            //proc.AddOutParameter("Ret", System.Data.DbType.Decimal, 18, 5);
            //proc.AddOutParameter("V", System.Data.DbType.String, 50);


            //DataTable dt = proc.ExecuteDataTable();


            //Response.Write(proc.OutParameters["Ret"].Value.ToString());
            //Response.Write("<br/>" + proc.OutParameters["V"].Value.ToString());
            //Response.Write("<br/>" + proc.ReturnValue.ToString());

            ////
            //foreach (DataRow dr in dt.Rows)
            //{
            //    Response.Write("<br/>" + dr["ConfigName"].ToString());
            //}

            //Easpnet.Modules.Models.PageInfo page = new Easpnet.Modules.Models.PageInfo();
            //PageParam p = new PageParam(1, 3);
            //Query q = new Query();
            //page.GetList(q, ref p);

            //Agent agent = new Agent();
            //Query q = Query.NewQuery();
            //q.Where("AgentId", Symbol.EqualTo, Ralation.End, 1);
            ////解除冻结
            //agent.Update("AgentStatus", AgentStatus.Freeze, q);

            //PageInfo page = new PageInfo();
            //Query q = Query.NewQuery();
            //object[] lst = new object[] { 1, 2, 3 };
            //q.Between("PageId", Ralation.End, 1,10);

            ////PageParam p = new PageParam(1, 5);
            //q.Select("PageId", "PageName");
            //List<Easpnet.Modules.ModelBase> ls = page.GetList(q, 1);

            //page.PageId = 1;
            //page.GetModel("PageName");
            //string pagename = page.PageName;

            //Transaction trans = new Transaction();
            //try
            //{
            //    Easpnet.Modules.Member.Models.User u = new Easpnet.Modules.Member.Models.User();
            //    u.UserName = "admin";
            //    //添加数据
            //    u.Create(trans);

            //    //更新数据
            //    u.Update(trans, "UserName", "adminff", 
            //        Query.NewQuery().Where("UserId", Symbol.EqualTo, Ralation.End, 20));                              

            //    trans.Commit();
            //}
            //catch
            //{
            //    trans.Rollback();
            //}

            //page.GetList(Query.NewQuery().Where("PageName", Symbol.Like, Ralation.End, "Admin"));

            //string s = Request.QueryString["s"];

            //PageInfo page = new PageInfo();
            //page.PageId = 1;

            //if (page.GetModel())
            //{
            //    PageInfo p = page.Clone() as PageInfo;

            //    page.PageName = "sdfsdfsd";
            //}
        }


    }
}
