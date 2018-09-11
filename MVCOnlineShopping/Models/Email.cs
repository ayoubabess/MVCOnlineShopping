using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVConlineshopping.Models
{
    public class Email
    {


        [Required]
        public int EmailID { get; set; }
        [DataType(DataType.EmailAddress), Display(Name = "De")]
            [Required]
        
        public string FromEmail { get; set; }
        [Required]
        [Display(Name = "Corps")]
            [DataType(DataType.MultilineText)]
            public string EMailBody { get; set; }
        [Required]
        [Display(Name = "Objet")]
            public string EmailSubject { get; set; }
           
        
        
    }
}