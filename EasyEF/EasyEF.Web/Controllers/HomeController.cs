using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyEF.Contract;
using EasyEF.WCFClientProxy;
using EasyEF.Models;
using EasyEF.Common;
using EasyEF.Utility;
using EasyEF.WCFExtension;

namespace EasyEF.Web.Controllers
{
    public class HomeController : Controller
    {
        public IServiceFactory ServiceFactory
        {
            get
            {
                //Need to inject dynamic later
                return new RemoteServiceFactory();
            }
        }
        
        public IService Service
        {
            get
            {
                return this.ServiceFactory.CreateService(); 
            }
        }

        public int PageSize
        {
            get
            {
                return 2;
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            WCFContext.Current.Operater = new Operater()
            {
                Name = "guozili",
                Time = DateTime.Now,
                IP = Fetch.UserIp,
                LoginToken = Guid.NewGuid(),
                Method = filterContext.ActionDescriptor.ActionName
            };
        }
        
        //
        // GET: /Home/

        public ActionResult Index(int id = 1)
        {
            var products = this.Service.GetProducts(PageSize, id);
            return View(products);
        }

    }
}
