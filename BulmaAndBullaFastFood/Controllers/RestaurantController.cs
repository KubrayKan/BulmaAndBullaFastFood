using BulmaAndBullaFastFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using MenuItem = BulmaAndBullaFastFood.Models.MenuItem;

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

        // GET: CustomerContact/Shop
        [HttpGet]
        public ActionResult Shop()
        {
            return View(db.MenuItems.ToList());
        }

        public ActionResult AddToCart(MenuItem item)
        {
            if(Session["Cart"] == null)
            {
                System.Diagnostics.Debug.WriteLine(item.name);
                List<MenuItem> itemsInCart = new List<MenuItem>();
                itemsInCart.Add(item);
                Session["Cart"] = itemsInCart;
            }
            else
            {
                List<MenuItem> itemsInCart = (List<MenuItem>)Session["Cart"];
                itemsInCart.Add(item);
                Session["Cart"] = itemsInCart;
                System.Diagnostics.Debug.WriteLine("new loop");
                foreach (MenuItem items in itemsInCart)
                {   
                    System.Diagnostics.Debug.WriteLine(items.description);
                }
            }

            return RedirectToAction("Shop");
        }

        // GET: CustomerContact/Cart
        [HttpGet]
        public ActionResult Cart()
        {
            List<MenuItem> itemsInCart = (List<MenuItem>)Session["Cart"];
            return View(itemsInCart);
        }

    }

}



