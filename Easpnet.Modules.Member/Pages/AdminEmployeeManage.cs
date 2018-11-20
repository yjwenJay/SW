using System;
using System.Collections.Generic;
using System.Text;
using Easpnet.Modules.Member.Models;

namespace Easpnet.Modules.Member.Pages
{
    public class AdminEmployeeManage : PageBase
    {
        protected List<AdminUser> lst;
        AdminUser user = new AdminUser();

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //处理数据
            Handler();


            //
            Query q = Query.NewQuery();
            q.Where("UserType", Symbol.GreaterThan, Ralation.End, 0);
            lst = user.GetList<AdminUser>(q);
        }

        /// <summary>
        /// 处理数据
        /// </summary>
        void Handler()
        {
            string option = Post("option");
            if (!string.IsNullOrEmpty(option))
            {
                if (!string.IsNullOrEmpty(Post("ids")))
                {
                    string[] arr = Post("ids").Split(new char[] { ',' });
                    if (arr.Length > 0)
                    {
                        object[] ids = new object[arr.Length];
                        for (int i = 0; i < arr.Length; i++)
                        {
                            ids[i] = TypeConvert.ToInt64(arr[i]);
                        }
                        Query q = Query.NewQuery();
                        q.In("UserId", Ralation.End, ids);

                        if (option == "Delete")
                        {
                            int c = user.Delete(q);
                        }
                        else if (option == "unbind")
                        {
                            if (user.Update(q, new NameObject("MacAddress", "")) == 0)
                            {
                                AddErrorMessage("解除绑定失败");
                            }
                        }
                    }
                }
            }

        }

    }
}
