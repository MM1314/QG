using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SOA.WLIMS.Models;
using SOA.WLIMS.Web.WCFService;

namespace SOA.WLIMS.Web.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        [Authorize]
        public ActionResult Index()
        {
            return View(ServiceFactory.GetUserService().Query(null));
        }

        //
        // GET: /User/Details/5

        public ActionResult Details(int id)
        {
            return View(ServiceFactory.GetUserService().Get(id));
        }

        //
        // GET: /User/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /User/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /User/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View(ServiceFactory.GetUserService().Get(id));
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        public ActionResult Edit(UserModel model)
        {
            try
            {
                // TODO: Add update logic here
                //User model = new User();
                //model.ID = id;
                //model.AliasName = collection["AliasName"];
                //model.Email = collection["AliasName"];
                //model.Tel = collection["AliasName"];
                //model.EmployeCode = collection["AliasName"];
                //model.IsDeleted = collection["IsDeleted"].ToLower().Equals("true");
                if (ServiceFactory.GetUserService().Modify(model))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("","修改失败");
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /User/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(ServiceFactory.GetUserService().Get(id));
        }

        //
        // POST: /User/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                if (ServiceFactory.GetUserService().Delete(id))
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
