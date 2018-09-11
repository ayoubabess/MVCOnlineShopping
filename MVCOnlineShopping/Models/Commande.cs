using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVConlineshopping.Models
{
    public class Commande
    {
       
            public int CommandeID { get; set; }
       
        [Display(Name = "état")]
        public string etat { get; set; }
        [Display(Name = "date Commande")]
        public DateTime dateCommande{ get; set; }
        [ForeignKey("Panier")]
        public int PanierID { get; set; }
        [ForeignKey("Client")]
        public int ClientID { get; set; }
        public virtual Panier Panier { set; get; }
        public virtual Client Client { set; get; }

       
    }
}