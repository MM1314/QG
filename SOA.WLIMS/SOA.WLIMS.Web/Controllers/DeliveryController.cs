using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SOA.WLIMS.Models;
using SOA.WLIMS.WCFClientProxy;

namespace SOA.WLIMS.Web.Controllers
{
    public class DeliveryController : Controller
    {
        //
        // GET: /Vehicle/
        public ActionResult Index()
        {
            return View(ServiceFactory.GetOrderService().QueryDelivery(null));
        }

        //
        // GET: /Vehicle/Details/5

        public ActionResult Details(int id)
        {
            return View(ServiceFactory.GetOrderService().GetDelivery(id));
        }

        //
        // GET: /Vehicle/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Vehicle/Create

        [HttpPost]
        public ActionResult Create(DeliveryInfoModel model)
        {
            try
            {
                // TODO: Add insert logic here
                if (ServiceFactory.GetOrderService().AddDelivery(model))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Vehicle/Edit/5

        public ActionResult Edit(int id)
        {
            return View(ServiceFactory.GetOrderService().GetDelivery(id));
        }

        //
        // POST: /Vehicle/Edit/5

        [HttpPost]
        public ActionResult Edit(DeliveryInfoModel model)
        {
            try
            {
                if (ServiceFactory.GetOrderService().ModifyDelivery(model))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "修改失败");
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Vehicle/Delete/5

        public ActionResult Delete(int id)
        {
            return View(ServiceFactory.GetOrderService().GetDelivery(id));
        }

        //
        // POST: /Vehicle/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                if (ServiceFactory.GetOrderService().DeleteDelivery(id))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
