using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tahri_Company.Models
{
    public class StockDepot
    {
        public int Id { get; set; }
        public int DepotId { get; set; }
        public int qte { get; set; }
        public string designation { get; set; }

    }
}