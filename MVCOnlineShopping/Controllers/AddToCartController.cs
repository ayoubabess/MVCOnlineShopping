using MVConlineshopping.DAL;
using MVConlineshopping.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVConlineshopping.Controllers
{
    public class AddToCartController : Controller
    {
        //DataTable dt;
        private IDal dal;
        Article _mdal = new Article();
        private ElectroventeContext db = new ElectroventeContext();
        private List<Category> allcategories;
        // GET: AddToCart  
        public PartialViewResult Myorder()
        {
            Panier panier = (Panier)Session["cart"];
            ViewBag.titre = "Panier";
            allcategories = db.Categories.ToList();
            decimal totale = 0;
            ViewBag.allcategories = allcategories;
            if (panier != null)
                foreach (var a in panier.Articlequantites)
                {
                    totale += a.Article.price * a.quantite;
                }

            ViewBag.totale = totale;
            return PartialView(panier);

        }

        public ActionResult Addquantite(int ID)
        {
            Article article = db.Articles.FirstOrDefault(c => c.ArticleID == ID);
            Panier panier = (Panier)Session["cart"];
            panier.Articlequantites.FirstOrDefault(x => x.Article.ArticleID == article.ArticleID).quantite++;
            ViewBag.cart = panier.Articlequantites.Count();


            Session["count"] = ViewBag.cart;
            Session["count"] = Convert.ToInt32(Session["count"]) - 1;
            decimal totale = 0;
            foreach (var a in panier.Articlequantites)
            {
                totale += a.Article.price * a.quantite;
            }
            ViewBag.totale = totale;

            return RedirectToAction("Myorder", "AddToCart");

        }
        public ActionResult Subquantite(int ID)
        {
            Article article = db.Articles.FirstOrDefault(c => c.ArticleID == ID);
            Panier panier = (Panier)Session["cart"];
            if (panier.Articlequantites.FirstOrDefault(x => x.Article.ArticleID == article.ArticleID).quantite > 1)
            { panier.Articlequantites.FirstOrDefault(x => x.Article.ArticleID == article.ArticleID).quantite--; }
            else
            {
                panier.Articlequantites.Remove(panier.Articlequantites.FirstOrDefault(x => x.Article.ArticleID == article.ArticleID));
            }
            ViewBag.cart = panier.Articlequantites.Count();
            decimal totale = 0;
            foreach (var a in panier.Articlequantites)
            {
                totale += a.Article.price * a.quantite;
            }
            ViewBag.totale = totale;
            Session["count"] = ViewBag.cart;
            Session["count"] = Convert.ToInt32(Session["count"]) - 1;
            return RedirectToAction("Myorder", "AddToCart");

        }
        public ActionResult Remove(int ID)
        {
            Article article = db.Articles.FirstOrDefault(c => c.ArticleID == ID);
            Panier panier = (Panier)Session["cart"];
            panier.Articlequantites.Remove(panier.Articlequantites.FirstOrDefault(x => x.Article.ArticleID == article.ArticleID));

            ViewBag.cart = panier.Articlequantites.Count();


            Session["count"] = ViewBag.cart;
            Session["count"] = Convert.ToInt32(Session["count"]) - 1;
            decimal totale = 0;
            foreach (var a in panier.Articlequantites)
            {
                totale += a.Article.price * a.quantite;
            }
            ViewBag.totale = totale;
            return RedirectToAction("Myorder", "AddToCart");


        }
        public ActionResult Add(int Id)
        {
            Article article = db.Articles.FirstOrDefault(c => c.ArticleID == Id);
            if (Session["cart"] == null)
            {
                var panier = new Panier();

                panier.Articlequantites.Add(new Articlequantite(article, 1));


                Session["cart"] = panier;
                ViewBag.cart = panier.Articlequantites.Count();


                Session["count"] = ViewBag.cart;


            }
            else
            {
                Panier panier = (Panier)Session["cart"];
                if (panier.Articlequantites.FirstOrDefault(x => x.Article.ArticleID == article.ArticleID) != null)
                { panier.Articlequantites.FirstOrDefault(x => x.Article.ArticleID == article.ArticleID).quantite++; }
                else
                    panier.Articlequantites.Add(new Articlequantite(article, 1));
                ViewBag.cart = panier.Articlequantites.Count();


                Session["count"] = ViewBag.cart;

            }
            return RedirectToAction("/", "Articles");


        }
        [Authorize]
        public PartialViewResult Commander()
        {
            try { 
            dal=new Dal();
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                Client client = dal.ObtenirUtilisateur(HttpContext.User.Identity.Name);
          
          
               Panier panier = (Panier)Session["cart"];
            db.Paniers.Add(panier);
                db.SaveChanges();
                Commande cmd = new Commande
                {
                    dateCommande = DateTime.Now,
                    etat = "Non validé",
                    PanierID = panier.PanierID,
                    Panier = panier,
                    Client = client,
                    ClientID = client.ClientID



                };
                db.Commandes.Add(cmd);
            db.SaveChanges() ;
                Session["cart"] = null;

            }
            ViewBag.result="success commande";
                return PartialView("Myorder", null);
            }
            catch(Exception ex)
            {
                ViewBag.result = "faillure commande";
                return PartialView("Myorder",null);
            }
        }
    }
}