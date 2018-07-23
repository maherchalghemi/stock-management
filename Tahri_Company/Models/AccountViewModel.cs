using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tahri_Company.Models
{
    public class AccountViewModel
    {
        public List<Commercial> Commercials { get; set; }
        public List<Magasinier> Magasiniers { get; set; }
        public List<Client> Clients { get; set; }
        public AccountViewModel()
        {
            Commercials = new List<Commercial>();
            Magasiniers = new List<Magasinier>();
            Clients = new List<Client>();
        }
    }
}