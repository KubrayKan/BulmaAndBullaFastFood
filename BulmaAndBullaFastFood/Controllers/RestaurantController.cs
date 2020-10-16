using BulmaAndBullaFastFood.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BulmaAndBullaFastFood.Controllers
{
    public class RestaurantController : Controller
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
            db.CustomerContacts.Add(entryToCreate);
            db.SaveChanges();

            return RedirectToAction("Contact");

        }

        // GET: CustomerContact/Menu
        [HttpGet]
        public ActionResult Menu()
        {
            return View(db.MenuItems.ToList());
        }

        // GET: Restaurant/Shop
        [HttpGet]
        public ActionResult Shop()
        {
            return View(db.MenuItems.ToList());
        }
        
        public ActionResult AddToCart(MenuItem item)
        {
            if (Session["Cart"] == null)
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
        
        // GET: Restaurant/Cart
        [HttpGet]
        public ActionResult Cart()
        {
            List<MenuItem> itemsInCart = (List<MenuItem>)Session["Cart"];
            return View(itemsInCart);
        }

        public ActionResult DeleteFromCart(MenuItem item)
        {
            List<MenuItem> itemsInCart = (List<MenuItem>)Session["Cart"];
            System.Diagnostics.Debug.WriteLine(itemsInCart.Remove(item));
            Session["Cart"] = itemsInCart;
            return RedirectToAction("Cart");
        }

        // POST: Restaurant/Payment
        [HttpGet]
        public ActionResult Payment()
        {
            List<MenuItem> itemsInCart = (List<MenuItem>)Session["Cart"];
            if (itemsInCart == null || itemsInCart.Count() == 0)
            {
                TempData["message"] = "Empty";
                return RedirectToAction("Cart", "Restaurant");
            }

            decimal subTotal = 0m;
            
            foreach(MenuItem item in itemsInCart)
            {
                subTotal += item.price;
            }

            decimal taxes = subTotal * 15 / 100;
            decimal delivery = subTotal * 15 / 100;
            
            if(subTotal > 25m)
            {
                delivery = 0m;
                System.Diagnostics.Debug.WriteLine(delivery);
            }

            decimal total = subTotal + taxes + delivery;

            Session["subtotal"] = Math.Round(subTotal, 2);
            Session["taxes"] = Math.Round(taxes, 2);
            Session["delivery"] = Math.Round(delivery, 2);
            Session["total"] = Math.Round(total, 2);

            return View(itemsInCart);
        }

        public ActionResult PaymentInfo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PaymentInfo([Bind(Exclude = "Id, user_Id, purchase_date, total_price")] OrderHistory orderEntry)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            if (!ModelState.IsValid) {
                return View();
            }
            orderEntry.user_Id = User.Identity.GetUserId();
            orderEntry.purchase_date = DateTime.Now;
            orderEntry.total_price = (decimal)Session["total"];
            db.OrdersHistory.Add(orderEntry);
            db.SaveChanges();
            Session.Clear();
            return RedirectToAction("OrderComplete", "Restaurant");
        }

        public ActionResult OrderComplete()
        {
            return View();
        }

    }
}