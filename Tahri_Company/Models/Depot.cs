using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tahri_Company.Models
{
    public class Depot
    {
        public int DepotId { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Adresse { get; set; }
        [Required]
        public string Tel { get; set; }
        
    }
}