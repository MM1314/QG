using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SOA.WLIMS.Models;
using SOA.WLIMS.Service.DAL;

namespace SOA.WLIMS.Web.Controllers
{
    public class StorehouseController : Controller
    {
        //
        // GET: /Storehouse/

        public ActionResult Index()
        {
            return View(ServiceFactory.GetStorehouseService().Query(null));
        }

        //
        // GET: /Storehouse/Details/5

        public ActionResult Details(int id)
        {
            return View(ServiceFactory.GetStorehouseService().Get(id));
        }

        //
        // GET: /Storehouse/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Storehouse/Create

        [HttpPost]
        public ActionResult Create(Storehouse model)
        {
            try
            {
                // TODO: Add insert logic here
                if (ServiceFactory.GetStorehouseService().Add(model))
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
        // GET: /Storehouse/Edit/5

        public ActionResult Edit(int id)
        {
            return View(ServiceFactory.GetStorehouseService().Get(id));
        }

        //
        // POST: /Storehouse/Edit/5

        [HttpPost]
        public ActionResult Edit(Storehouse model)
        {
            try
            {
                if (ServiceFactory.GetStorehouseService().Modify(model))
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
        // GET: /Storehouse/Delete/5

        public ActionResult Delete(int id)
        {
            return View(ServiceFactory.GetStorehouseService().Get(id));
        }

        //
        // POST: /Storehouse/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                if (ServiceFactory.GetStorehouseService().Delete(id))
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
