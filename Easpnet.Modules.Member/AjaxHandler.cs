using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Easpnet.Modules.Member
{
    public class AjaxHandler : IAjaxHandler
    {

        #region IAjaxHandler 成员

        public void Ajax()
        {
            switch (HttpContext.Current.Request.QueryString["action"])
            {
                case "validate_user_name":
                    validate_user_name();
                    break;
                default:
                    break;
            }
        }

        private void validate_user_name()
        {
            string validateValue = HttpContext.Current.Request.Form["validateValue"];
            string validateId = HttpContext.Current.Request.Form["validateId"];
            string validateError = HttpContext.Current.Request.Form["validateError"];

            Query query = Query.NewQuery();
            query.AddCondition(Query.CreateCondition("UserName", Symbol.EqualTo, Ralation.End, HttpContext.Current.Request.Form["validateValue"]));
            int count = new Models.User().Count(query);

            JSONHelper json = new JSONHelper();
            json.addItem("validateId", validateId.ToString());
            json.addItem("validateError", validateError);
            json.successS = count == 0;
            json.addItemOk();
            HttpContext.Current.Response.Write(json.ToString());
        }

        #endregion
    }
}


//{"totalCount":"0","error":"","success":"True","info":"ffffff","data":[{"a":"1","b":"2"},{"a":"13","b":"24"}]}