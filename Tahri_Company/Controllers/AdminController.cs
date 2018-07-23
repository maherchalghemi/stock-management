using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Tahri_Company.Models;

namespace Tahri_Company.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Taleau de bord Admin
        [Authorize(Roles = "Administrateur")]
        public ActionResult Index()
        {
            return View();
        }

        //Creér un nouveau Commercial
        [Authorize(Roles = "Administrateur")]
        public ActionResult CreateCommercial()
        {
            return View();
        }

        // GET: Liste des Commercials
        [Authorize(Roles = "Administrateur")]
        public ActionResult CommercialsList()
        {
            var Db = new ApplicationDbContext();
            var users = Db.Users.ToList();
            var commercialRole = Db.Roles.FirstOrDefault(r => r.Name == "Commercial");
            var model = new AccountViewModel();
            foreach (var user in users)
            {
                if (user.Roles.First().RoleId == commercialRole.Id)
                {
                    model.Commercials.Add(new Commercial(user));
                }
            }
            return View(model);
        }

        [Authorize(Roles = "Administrateur")]
        //Modifier un Commercial ( Par l'administrateur )  
        public ActionResult EditCommercial(string id, Tahri_Company.Controllers.ManageController.ManageMessageId? Message = null)
        {
            var Db = new ApplicationDbContext();
            var user = Db.Users.FirstOrDefault(u => u.Email == id);
            var model = new Commercial(user);
            ViewBag.MessageId = Message;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCommercial(Commercial model)
        {
            if (ModelState.IsValid)
            {
                var Db = new ApplicationDbContext();
                var user = Db.Users.First(u => u.Email == model.Email);
                // Update the user data:

                user.Nom = model.Nom;
                user.Prenom = model.Prenom;
                user.BirthDate = model.BirthDate;
                user.TelPerso = model.TelPerso;
                user.Adresse = model.Adresse;             
                Db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                await Db.SaveChangesAsync();
                return RedirectToAction("CommercialsList");
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [Authorize(Roles = "Administrateur")]
        ////Supprimer un Commercial ( Par l'administrateur )  
        public ActionResult DeleteCommercial(string id)
        {
            var Db = new ApplicationDbContext();
            var user = Db.Users.First(u => u.Email == id);
            var model = new Commercial(user);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [Authorize(Roles = "Administrateur")]
        [HttpPost, ActionName("DeleteCommercial")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCommercialConfirmed(string id)
        {
            var Db = new ApplicationDbContext();
            var user = Db.Users.First(u => u.Email == id);
            Db.Users.Remove(user);
            Db.SaveChanges();
            return RedirectToAction("CommercialsList");
        }

        //Creér un nouveau Magasinier
        [Authorize(Roles = "Administrateur")]
        public ActionResult CreateMagasinier()
        {
            return View();
        }

        // GET: Liste des Magasiniers
        [Authorize(Roles = "Administrateur")]
        public ActionResult MagasiniersList()
        {
            var Db = new ApplicationDbContext();
            var users = Db.Users.ToList();
            var magasinierRole = Db.Roles.FirstOrDefault(r => r.Name == "Magasinier");
            var model = new AccountViewModel();
            foreach (var user in users)
            {
                if (user.Roles.First().RoleId == magasinierRole.Id)
                {
                    model.Magasiniers.Add(new Magasinier(user));
                }
            }
            return View(model);
        }

        
        //Modifier un Commercial ( Par l'administrateur )  
        [Authorize(Roles = "Administrateur")]
        public ActionResult EditMagasinier(string id, Tahri_Company.Controllers.ManageController.ManageMessageId? Message = null)
        {
            var Db = new ApplicationDbContext();
            var user = Db.Users.FirstOrDefault(u => u.Email == id);
            var model = new Magasinier(user);
            ViewBag.MessageId = Message;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditMagasinier(Magasinier model)
        {
            if (ModelState.IsValid)
            {
                var Db = new ApplicationDbContext();
                var user = Db.Users.First(u => u.Email == model.Email);
                // Update the user data:

                user.Nom = model.Nom;
                user.Prenom = model.Prenom;
                user.BirthDate = model.BirthDate;
                user.TelPerso = model.TelPerso;
                user.Adresse = model.Adresse;
                Db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                await Db.SaveChangesAsync();
                return RedirectToAction("MagasiniersList");
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        ////Supprimer un Commercial ( Par l'administrateur )
        [Authorize(Roles = "Administrateur")]
        public ActionResult DeleteMagasinier(string id)
        {
            var Db = new ApplicationDbContext();
            var user = Db.Users.First(u => u.Email == id);
            var model = new Magasinier(user);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [Authorize(Roles = "Administrateur")]
        [HttpPost, ActionName("DeleteMagasinier")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteMagasinierConfirmed(string id)
        {
            var Db = new ApplicationDbContext();
            var user = Db.Users.First(u => u.Email == id);
            Db.Users.Remove(user);
            Db.SaveChanges();
            return RedirectToAction("MagasiniersList");
        }



        //Creér un nouveau produit
        [Authorize(Roles = "Administrateur")]
        public ActionResult CreateProduit()
        {
            return View();
        }

        // POST: Creér un nouveau produit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProduit([Bind(Include = "ProduitId,Marque,Libelle,PrixHT,PrixTTC,Gratuite,FormuleGrX,FormuleGrY")] Produit produit)
        {
            if(produit.Gratuite == "on")
            { produit.Gratuite = "true"; }
            else
            { produit.Gratuite = "false"; }

            if (ModelState.IsValid)
            { 
                db.Produits.Add(produit);
                db.SaveChanges();
                return RedirectToAction("ProduitsList");
            }

            return View(produit);
        }

        // GET: Liste des Produits
        [Authorize(Roles = "Administrateur")]
        public ActionResult ProduitsList()
        {
            var produits = db.Produits;
            return View(produits.ToList());
        }

    }
}