using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TwoFactorAuth.Models
{
    public class TwoFactor
    {
        [Required]
        public string TwoFactorCode { get; set; }

        public string Email { get; set; }

        public string code { get; set; }
    }
}
