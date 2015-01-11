using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SOA.WLIMS.Models;

namespace SOA.WLIMS.Web.Controllers
{
    public class VehicleController : Controller
    {
        //
        // GET: /Vehicle/
        [Authorize]
        public ActionResult Index()
        {
            return View(ServiceFactory.GetVehicleService().Query(null));
        }

        //
        // GET: /Vehicle/Details/5

        public ActionResult Details(int id)
        {
            return View(ServiceFactory.GetVehicleService().Get(id));
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
        public ActionResult Create(VehicleModel model)
        {
            try
            {
                // TODO: Add insert logic here
                if (ServiceFactory.GetVehicleService().Add(model))
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
            return View(ServiceFactory.GetVehicleService().Get(id));
        }

        //
        // POST: /Vehicle/Edit/5

        [HttpPost]
        public ActionResult Edit(VehicleModel model)
        {
            try
            {
                if (ServiceFactory.GetVehicleService().Modify(model))
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
            return View(ServiceFactory.GetVehicleService().Get(id));
        }

        //
        // POST: /Vehicle/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                if (ServiceFactory.GetVehicleService().Delete(id))
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
