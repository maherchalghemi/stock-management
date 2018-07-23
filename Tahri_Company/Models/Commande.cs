using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tahri_Company.Models
{
    public class Commande
    {
        public int Id { get; set; }
        [Required]
        public int CommertialId { get; set; }
        [Required]
        public string NomCli { get; set; }
        [Required]
        public string AdresseCli { get; set; }
        [Required]
        public string TelCli { get; set; }
    }
}