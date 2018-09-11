using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVConlineshopping.Models
{
    public class Client
    {
        public int ClientID { get; set; }
        [Required]
        [Display(Name = "Nom et Prénom")]
        public string Prenom { get; set; }
        [Required]
        [Display(Name = "adresse email")]
        public string Email { get; set; }
        [Display(Name = "login ")]
        public string login { get; set; }
         [Display(Name = "Mot de passe")]
        public string MotDePasse { get; set; }

    }
}