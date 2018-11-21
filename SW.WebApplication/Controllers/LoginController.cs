using SW.Business;
using SW.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SW.WebApplication.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Logins()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CheckLogin()
        {
            string account = Request.Form["logName"];
            string password = Request.Form["logPass"];

            SysUserAuthorBLL sysUserAuthorBLL = new SysUserAuthorBLL();
            SysUser_Role_Module_Authorization sysUser_Role =   sysUserAuthorBLL.CheckAccount(account);
            if (sysUserAuthorBLL.CheckPasswordHash(password,sysUser_Role))
            {
                return  RedirectToAction("Index", "Portal");
                //return RedirectToRoute(new { controller = "Home", action = "Index" });
            } 
            return Content("<script>alert('账号密码错误，请重试！');</script>"); 
        }
        public ActionResult Regis()
        {
            return View();
        }
    }
}