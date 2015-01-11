using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SOA.WLIMS.Models;

namespace SOA.WLIMS.Web.Controllers
{
    public class OrderController : Controller
    {
        //
        // GET: /Order/
        public ActionResult Index()
        {
            return View(ServiceFactory.GetOrderService().Query(null));
        }

        //
        // GET: /Order/Details/5

        public ActionResult Details(int id)
        {
            return View(ServiceFactory.GetOrderService().Get(id));
        }

        //
        // GET: /Order/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Order/Create

        [HttpPost]
        public ActionResult Create(OrderModel model)
        {
            try
            {
                // TODO: Add insert logic here
                if (ServiceFactory.GetOrderService().Add(model))
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
        // GET: /Order/Edit/5

        public ActionResult Edit(int id)
        {
            return View(ServiceFactory.GetOrderService().Get(id));
        }

        //
        // POST: /Order/Edit/5

        [HttpPost]
        public ActionResult Edit(OrderModel model)
        {
            try
            {
                if (ServiceFactory.GetOrderService().Modify(model))
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
        // GET: /Order/Delete/5

        public ActionResult Delete(int id)
        {
            return View(ServiceFactory.GetOrderService().Get(id));
        }

        //
        // POST: /Order/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                if (ServiceFactory.GetOrderService().Delete(id))
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
