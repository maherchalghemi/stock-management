using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tahri_Company.Models
{
    public class ItemCmd
    {
        public int ItemCmdId { get; set; }
        [Required]
        public int CmdId { get; set; }
        [Required]
        public int qte { get; set; }
        [Required]
        public string depot { get; set; }

    }
}