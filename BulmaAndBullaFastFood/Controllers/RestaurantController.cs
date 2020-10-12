using BulmaAndBullaFastFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BulmaAndBullaFastFood.Controllers
{
    public class CustomerContactController : Controller
    {
        private restaurant_dbEntities db = new restaurant_dbEntities();

        // GET: CustomerContact/Contact
        public ActionResult Contact()
        {
            return View();
        }

        // POST: CustomerContact/Contact
        [HttpPost]
        public ActionResult Contact([Bind(Exclude = "Id")] CustomerContact entryToCreate)
        {
            if (!ModelState.IsValid)
                return View();
            db.CustomerContactsEntities.Add(entryToCreate);
            db.SaveChanges();

            return RedirectToAction("Contact");

        }

        // GET: CustomerContact/Menu
        [HttpGet]
        public ActionResult Menu()
        {
            return View(db.MenuItems.ToList());
        }

        }
}
