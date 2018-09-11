using MVConlineshopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVConlineshopping.DAL
{
    public interface IDal : IDisposable
    {
      
        int AjouterUtilisateur(Client client);
        Client Authentifier(string nom, string motDePasse);
        Client ObtenirUtilisateur(int id);
        Client ObtenirUtilisateur(string idStr);
    }
}
