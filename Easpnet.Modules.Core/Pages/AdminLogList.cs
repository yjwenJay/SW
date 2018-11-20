using System;
using System.Collections.Generic;
using System.Text;
using Easpnet.Modules.Member.Models;
using Easpnet.Modules.Member;

namespace Easpnet.Modules.Core.Pages
{
    public class AdminLogList : PageBase
    {
        protected global::Easpnet.Controls.Pager AspNetPager1;        
        protected List<Log> lst;
        protected List<NameValue> lstUser;
        protected List<NameValue> lstTitle;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //获取用户列表
            lstUser = new List<NameValue>();
            lstUser.Add(new NameValue("所有日志", ""));
            lstUser.Add(new NameValue("系统日志", "0"));
            Query q = Query.NewQuery();
            q.Where("UserType", Symbol.EqualTo, Ralation.Or, UserType.SystemAdmin);
            q.Where("UserType", Symbol.EqualTo, Ralation.Or, UserType.Employee);
            q.Where("UserType", Symbol.EqualTo, Ralation.End, UserType.WebAdmin);
            List<User> lst = new User().GetList<User>(q);
            foreach (ModelBase item in lst)
            {
                User u = item as User;
                NameValue nv = new NameValue(u.UserName, u.UserId.ToString());
                lstUser.Add(nv);
            }

            //
            lstTitle = new List<NameValue>();
            lstTitle.Add(new NameValue("所有类别", ""));
            System.Collections.Generic.List<string> lstC = LogReader.ReadTitles();
            foreach (string item in lstC)
            {
                lstTitle.Add(new NameValue(item, item));
            }

            //
            AspNetPager1.PageSize = TypeConvert.ToInt32(Configs["admin_list_page_size"]);
        }

        /// <summary>
        /// 数据查询
        /// </summary>
        /// <param name="src"></param>
        /// <param name="e"></param>
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            LogQuery query = new LogQuery();

            string from = Get("from");
            string to = Get("to");
            string level = Get("level");
            string operation = Get("operation");
            string title = Get("title");
            string manager = Get("manager");
            string primaryData = Get("primaryData");
            string message = Get("message");

            #region 构造条件
            query.Title = title;
            query.UserID = manager;
            query.PrimaryData = primaryData;
            query.Message = message;
            if (!string.IsNullOrEmpty(from))
            {
                try
                {
                    query.DateTimeFrom = Convert.ToDateTime(from);
                }
                catch (Exception)
                {
                    query.DateTimeFrom = default(DateTime);
                }
            }

            if (!string.IsNullOrEmpty(to))
            {
                try
                {
                    query.DateTimeTo = Convert.ToDateTime(to);
                }
                catch (Exception)
                {
                    query.DateTimeTo = default(DateTime);
                }
            }

            if (!string.IsNullOrEmpty(level))
            {
                try
                {
                    query.Level = (LogLevel)Convert.ToInt32(level);
                }
                catch (Exception)
                {
                    query.Level = LogLevel.Null;
                }
            }

            if (!string.IsNullOrEmpty(operation))
            {
                try
                {
                    query.Operation = (LogOperation)Convert.ToInt32(operation);
                }
                catch (Exception)
                {
                    query.Operation = LogOperation.Null;
                }
            }

            #endregion
            query.PageIndex = Convert.ToInt32(Get("pageindex"));
            query.PageSize = AspNetPager1.PageSize;
            lst = LogReader.ReadLog(ref query);            
            AspNetPager1.RecordCount = query.TotalRecord;
            AspNetPager1.CurrentPageIndex = query.PageIndex;
        }
    }
}
