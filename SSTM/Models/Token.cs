using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SSTM.Models
{
    public class Token
    {
        [Key]
        public Guid token { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public bool used { get; set; } //Default 0
    }
}