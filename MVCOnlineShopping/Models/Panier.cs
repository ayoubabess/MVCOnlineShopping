using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVConlineshopping.Models
{
    public class Panier
    {
       

        public int PanierID { get; set; }
        //[Display(Name = "libellé")]
       
        public virtual ICollection<Articlequantite> Articlequantites { set; get; }
        public Panier()
        {
            Articlequantites = new HashSet<Articlequantite>();
        }
    }
    public class Articlequantite
    {
        public int ArticlequantiteID { get; set; }
        public int quantite;
       

        public Articlequantite(Article article, int quantite)
        {
            Article = article;
            this.quantite = quantite;
        }

        public Article Article{ set; get; }


}
    }
