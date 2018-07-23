using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tahri_Company.Models;

namespace Tahri_Company.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact([Bind(Include = "ImpressionId,Nom,Email,Titre,Message")] Impression impression)
        {
            if (ModelState.IsValid)
            {
                db.Impressions.Add(impression);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(impression);
        }
    }
}