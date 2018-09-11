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
using PagedList;

namespace MVConlineshopping.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private const int DefaultPageSize = 10;
        private ElectroventeContext db = new ElectroventeContext();
        private List<Category> allcategories;
        private List<Article> allArticle;

        // GET: Articles
        public ActionResult Index(int? page)
        {
            
            allArticle = db.Articles.ToList();
            allcategories = db.Categories.ToList();
            int currentPageIndex = page.HasValue ? page.Value : 1;
            ViewBag.allcategories = allcategories;
            IList<Category> alc = db.Categories.ToList();
              IList<Marque> alm = db.Marques.ToList();
               ViewData["alcategories"] = alc;
                   ViewData["almarques"] = alm;
            return View("index", "~/Views/Shared/LayoutAdmin.cshtml",allArticle.ToPagedList(currentPageIndex, DefaultPageSize));
        }
        // [ChildActionOnly]
        
        //public ActionResult CategoryMarque()
        //{
        //    //CategoryMarque CategoryMarque = new CategoryMarque();
        //    //CategoryMarque.Marques= db.Marques.ToList();
        //    //CategoryMarque.Categories= db.Categories.ToList();
        //    IList<Category> alc= db.Categories.ToList();
        //    IList<Marque> alm = db.Marques.ToList();
        //    ViewData["alcategories"] = alc;
        //        ViewData["almarques"] = alm;
            
        //    return View();
        //}

        public ActionResult ViewByCategory(string categoryName, int? page)
        {

            IList<Category> alc = db.Categories.ToList();
            IList<Marque> alm = db.Marques.ToList();
            ViewData["alcategories"] = alc;
            ViewData["almarques"] = alm;


            allcategories = db.Categories.ToList();
            ViewBag.allcategories = allcategories;
            int currentPageIndex = page.HasValue ? page.Value : 1;
           
             var category = db.Categories.First(c => c.name.Equals(categoryName));
            var productsByCategory = category.Articles.ToPagedList(currentPageIndex,DefaultPageSize);
            /*ViewBag.CategoryName = new SelectList(this.allCategories, categoryName);*/
           
            return View(productsByCategory);
        }
        public ActionResult ViewByMarque(string marqueName, int? page)
        {
            IList<Category> alc = db.Categories.ToList();
            IList<Marque> alm = db.Marques.ToList();
            ViewData["alcategories"] = alc;
            ViewData["almarques"] = alm;



            allcategories = db.Categories.ToList();
            ViewBag.allcategories = allcategories;
            int currentPageIndex = page.HasValue ? page.Value : 1;

            var marque = db.Marques.First(c => c.name.Equals(marqueName));
            var productsByCategory = marque.Articles.ToPagedList(currentPageIndex, DefaultPageSize);
            /*ViewBag.CategoryName = new SelectList(this.allCategories, categoryName);*/

            return View(productsByCategory);
        }
        // GET: Articles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            allcategories = db.Categories.ToList();
            ViewBag.allcategories = allcategories;
            return View(article);
        }

        // GET: Articles/Create
        public ActionResult Create()
        {
            ViewData["MarqueID"] = new SelectList(db.Marques.ToList(), "MarqueID", "Name");
            ViewData["CategoryID"] = new SelectList(db.Categories.ToList(), "CategoryID", "Name");
            allcategories = db.Categories.ToList();
            ViewBag.allcategories = allcategories;
            return View();
        }

        // POST: Articles/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Article article, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file.ContentLength > 0)
                {
                    article.image = new byte[file.ContentLength];
                    file.InputStream.Read(article.image, 0, file.ContentLength);

                    db.Articles.Add(article);
                    //db.Categories.First(c => c.CategoryID.Equals(Category)).Articles.Add(article);
                    db.SaveChanges();

                }

                allcategories = db.Categories.ToList();
                ViewBag.allcategories = allcategories;
                return RedirectToAction("~/admin/Index");
            }
            ViewData["MarqueID"] = new SelectList(db.Marques.ToList(), "MarqueID", "Name");
            ViewData["CategoryID"] = new SelectList(db.Categories.ToList(), "CategoryID", "Name");
            allcategories = db.Categories.ToList();
            ViewBag.allcategories = allcategories;
            return View(article);
        }

        // GET: Articles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            allcategories = db.Categories.ToList();
            ViewBag.allcategories = allcategories;
            ViewData["MarqueID"] = new SelectList(db.Marques.ToList(), "MarqueID", "Name",article.MarqueID);
            ViewData["CategoryID"] = new SelectList(db.Categories.ToList(), "CategoryID", "Name", article.CategoryID);
            return View(article);
        }

        // POST: Articles/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Article Article, HttpPostedFileBase file )
        {
            if (ModelState.IsValid)
            {
                Article.image = null;
                //if (file.ContentLength > 0)
                if (file != null)
                {
                    Article.image = new byte[file.ContentLength];
                    file.InputStream.Read(Article.image, 0, file.ContentLength);
                }


                db.Entry(Article).State = EntityState.Modified;

                if (file == null)
                    db.Entry(Article).Property("image").IsModified = false;
               
                
                db.SaveChanges();
                allcategories = db.Categories.ToList();
                ViewBag.allcategories = allcategories;
                return RedirectToAction("Index", "Admin");
            }
            ViewData["MarqueID"] = new SelectList(db.Marques.ToList(), "MarqueID", "Name",Article.CategoryID);
            ViewData["CategoryID"] = new SelectList(db.Categories.ToList(), "CategoryID", "Name", Article.MarqueID);

            allcategories = db.Categories.ToList();
            ViewBag.allcategories = allcategories;
            return View(Article);
        }

        // GET: Articles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            allcategories = db.Categories.ToList();
            ViewBag.allcategories = allcategories;
            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
            db.SaveChanges();
            allcategories = db.Categories.ToList();
            ViewBag.allcategories = allcategories;
            return RedirectToAction("Index");
        }
        public ActionResult gerercommandes()
        {
            allcategories = db.Categories.ToList();

            ViewBag.allcategories = allcategories;
            return View(db.Commandes.ToList());
        }
        public ActionResult Validercommande(int id)
        {
            allcategories = db.Categories.ToList();

            ViewBag.allcategories = allcategories;
           Commande commande= db.Commandes.FirstOrDefault(x => x.CommandeID == id);
            commande.etat = "validé";
            db.SaveChanges();
            ViewBag.message = "success";
            return View("gerercommandes", db.Commandes.ToList());
        }
        public ActionResult Detailcommandes(int id)
        {
            allcategories = db.Categories.ToList();

            ViewBag.allcategories = allcategories;
            return View(db.Commandes.ToList());
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
