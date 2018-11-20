using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace SW.Controllers
{
    public class BaseApiController : ApiController
    {
        /// <summary>
        /// 获取get请求参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected string QueryString(string value)
        {
            HttpContextBase context = (HttpContextBase)Request.Properties["MS_HttpContext"];//获取传统context
            HttpRequestBase request = context.Request;//定义传统request对象
            return request.QueryString[value];
        }

        /// <summary>
        /// 获取post请求参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected string Form(string value)
        {
            HttpContextBase context = (HttpContextBase)Request.Properties["MS_HttpContext"];//获取传统context
            HttpRequestBase request = context.Request;//定义传统request对象
            return request.Form[value];
        }

    }
}
