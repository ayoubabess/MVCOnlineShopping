using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVConlineshopping.Models
{
    public class ClientViewModel
    {
        public Client Utilisateur { get; set; }
        public bool Authentifie { get; set; }
    }
}