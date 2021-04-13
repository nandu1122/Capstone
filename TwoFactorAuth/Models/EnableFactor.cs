using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TwoFactorAuth.Models
{
    public class EnableFactor
    {
        [Key]
        public string Id { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }

        public string Email { get;  set; }

        public string code { get; set; }

    }
}
