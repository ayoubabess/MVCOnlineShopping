using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace MVConlineshopping.Models
{
    public class Article
    {
        [Required]
        public int ArticleID { get; set; }
        [Display(Name = "Libellé")]
        [Required]
        public string name { get; set; }
        [Required]
        public string Description { get; set; }
        [Display(Name = "categorie")]
        [Required]
        [ForeignKey("Category")]
        public int CategoryID { get; set; }
        [Required]
        [ForeignKey("Marque")]
        public int MarqueID { get; set; }
        public Category Category { get; set; }
        public Marque Marque { get; set; }
        [Display(Name = "Prix")]
        public decimal price { get; set; }
        [Display(Name = "Ancien prix")]
        public decimal oldeprice { get; set; }

        public byte[] image { get; set; }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {if (byteArrayIn == null)
                return null;
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
       
    }
  
}