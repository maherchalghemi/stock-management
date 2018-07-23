using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tahri_Company.Models
{
    public class Impression
    {
        public int ImpressionId { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Titre { get; set; }
        [Required]
        public string Message { get; set; }

    }
}