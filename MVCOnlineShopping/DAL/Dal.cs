using MVConlineshopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;

namespace MVConlineshopping.DAL
{
    public class Dal:IDal
    {
        private ElectroventeContext bdd;

        public Dal()
        {
         bdd = new ElectroventeContext();
       
        }
        public int AjouterUtilisateur(Client client)
        {
          
            bdd.Clients.Add(client);
            bdd.SaveChanges();
            return client.ClientID;
        }

        public Client Authentifier(string nom, string motDePasse)
        {
            string motDePasseEncode =(motDePasse);
            return bdd.Clients.FirstOrDefault(u => u.login== nom && u.MotDePasse == motDePasseEncode);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Client ObtenirUtilisateur(int id)
        {
            return bdd.Clients.FirstOrDefault(u => u.ClientID == id);
        }

        public Client ObtenirUtilisateur(string idString)
        {
            int id;
            if (int.TryParse(idString, out id))
                return ObtenirUtilisateur(id);
            return null;
        }
    }
}