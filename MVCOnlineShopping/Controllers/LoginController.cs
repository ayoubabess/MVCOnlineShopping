using MVConlineshopping.DAL;
using MVConlineshopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;

using System.Web.Security;

namespace MVConlineshopping.Controllers
{
    public class LoginController : Controller
    {
        private IDal dal;
        private ElectroventeContext db = new ElectroventeContext();
        private List<Category> allcategories;

        public LoginController() : this(new Dal())
        {

        }

        private LoginController(IDal dalIoc)
        {
            dal = dalIoc;
        }

        public ActionResult Index()
        {
            ClientViewModel viewModel = new ClientViewModel { Authentifie = HttpContext.User.Identity.IsAuthenticated };
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                viewModel.Utilisateur = dal.ObtenirUtilisateur(HttpContext.User.Identity.Name);
            }
            allcategories = db.Categories.ToList();

            ViewBag.allcategories = allcategories;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(ClientViewModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                Client utilisateur = dal.Authentifier(viewModel.Utilisateur.Prenom, viewModel.Utilisateur.MotDePasse);
                if (utilisateur != null)
                {
                    FormsAuthentication.SetAuthCookie(utilisateur.ClientID.ToString(), false);
                    if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);
                    return Redirect("/");
                }
                ModelState.AddModelError("Utilisateur.Prenom", "Prénom et/ou mot de passe incorrect(s)");
            }
            allcategories = db.Categories.ToList();
            
            ViewBag.allcategories = allcategories;
            return View(viewModel);
        }

        public ActionResult CreerCompte()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreerCompte(Client utilisateur)
        {
            if (ModelState.IsValid)
            {
                int id = dal.AjouterUtilisateur(utilisateur);
                FormsAuthentication.SetAuthCookie(id.ToString(), false);
                return Redirect("/");
            }
            return View(utilisateur);
        }

        public ActionResult SendEmail()
        {
            ViewBag.titre = "Contact";
            return View();
        }

        [HttpPost]
        public ActionResult SendEmail(Email obj)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    //Configuring webMail class to send emails  
                    //gmail smtp server  
                    WebMail.SmtpServer = "smtp.gmail.com";
                    //gmail port to send emails  
                    WebMail.SmtpPort = 587;
                    WebMail.SmtpUseDefaultCredentials = true;
                    //sending emails with secure protocol  
                    WebMail.EnableSsl = true;
                    //EmailId used to send emails from application  
                    WebMail.UserName = "ayoub.abess@gmail.com";
                    WebMail.Password = "jeanvaljean 1993";

                    //Sender email address.  
                    WebMail.From = obj.FromEmail;

                    //Send email  
                    WebMail.Send(to: "ayoub.abess@gmail.com", from: obj.FromEmail, subject: obj.EmailSubject, body: obj.EMailBody, isBodyHtml: true);
                    ViewBag.Status = "Email Sent Successfully.";
                }
            }
            catch (Exception)
            {
                ViewBag.Status = "Problem while sending email, Please check details.";

            }
            return View();
        }
    
    public ActionResult Deconnexion()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }
    }
}