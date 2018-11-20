using SW.Commons.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SW.Controllers
{
    public class AdminCMSController : Controller
    {
        // GET: AdminCMS
        public ActionResult Index()
        { 
            return View();
        }
        public ActionResult Welcome()
        {
            return View();
        }
    }
}