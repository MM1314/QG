using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOA.WLIMS.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "欢迎使用【赵强】SOA物流信息管理系统!";

            return View();
        }
        public ActionResult About()
        {
            return View();
        }
    }
}
