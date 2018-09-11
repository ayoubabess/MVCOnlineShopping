using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVConlineshopping.Models
{
    public class Marque
    {
        public int MarqueID { get; set; }
        [Display(Name = "Libellé")]
        [Required]
        public string name { get; set; }
        [Required]
        public string Description { get; set; }
        public virtual ICollection<Article> Articles { set; get; }
    }
}