using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVConlineshopping.DAL;
using MVConlineshopping.Models;

namespace MVConlineshopping.Controllers
{
    public class MarquesController : Controller
    {
        private List<Category> allcategories;
        private ElectroventeContext db = new ElectroventeContext();

        // GET: Marques
        public ActionResult Index()
        {
            allcategories = db.Categories.ToList();
            ViewBag.allcategories = allcategories;
            return View("Index","~/Views/Shared/LayoutAdmin.cshtml", db.Marques.ToList());
        }

        // GET: Marques/Details/5
        public ActionResult Details(int? id)
        {
            allcategories = db.Categories.ToList();
            ViewBag.allcategories = allcategories;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marque marque = db.Marques.Find(id);
            if (marque == null)
            {
                return HttpNotFound();
            }
            return View(marque);
        }

        // GET: Marques/Create
        public ActionResult Create()
        {
            allcategories = db.Categories.ToList();
            ViewBag.allcategories = allcategories;
            return View();
        }

        // POST: Marques/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MarqueID,name,Description")] Marque marque)
        {
            allcategories = db.Categories.ToList();
            ViewBag.allcategories = allcategories;
            if (ModelState.IsValid)
            {
                db.Marques.Add(marque);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(marque);
        }

        // GET: Marques/Edit/5
        public ActionResult Edit(int? id)
        {
            allcategories = db.Categories.ToList();
            ViewBag.allcategories = allcategories;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marque marque = db.Marques.Find(id);
            if (marque == null)
            {
                return HttpNotFound();
            }

            return View(marque);
        }

        // POST: Marques/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MarqueID,name,Description")] Marque marque)
        {
            allcategories = db.Categories.ToList();
            ViewBag.allcategories = allcategories;
            if (ModelState.IsValid)
            {
                db.Entry(marque).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(marque);
        }

        // GET: Marques/Delete/5
        public ActionResult Delete(int? id)
        {
            allcategories = db.Categories.ToList();
            ViewBag.allcategories = allcategories;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marque marque = db.Marques.Find(id);
            if (marque == null)
            {
                return HttpNotFound();
            }
            return View(marque);
        }

        // POST: Marques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            allcategories = db.Categories.ToList();
            ViewBag.allcategories = allcategories;
            Marque marque = db.Marques.Find(id);
            db.Marques.Remove(marque);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
