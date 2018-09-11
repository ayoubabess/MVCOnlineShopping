using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVConlineshopping.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        [Display(Name = "libellé")]
        [Required]
        public string name { get; set; }
        [Required]
        public string Description { get; set; }
        public virtual ICollection<Article> Articles { set; get; }
    }
}