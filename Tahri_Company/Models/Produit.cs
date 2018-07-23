using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tahri_Company.Models
{
    public enum Marques
    {
        Kamill, Glysolid
    }
    public class Produit
    {
        public int ProduitId { get; set; }
        [Required]
        public Marques? Marque { get; set; }
        [Required]
        public string Libelle { get; set; }
        [Required]
        public decimal PrixHT { get; set; }
        [Required]
        public decimal PrixTTC { get; set; }
       
        //Formule Gratuité ( Exemple : 10 + 2 Grauits ( 10 -> X ; 2 -> Y ) )
        public string Gratuite { get; set; }
        public decimal? FormuleGrX { get; set; }
        public decimal? FormuleGrY { get; set; }


    }
}