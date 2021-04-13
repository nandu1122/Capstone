using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TwoFactorAuth.Models
{
    public class AppUser : IdentityUser
    {
        [StringLength(50)]
        public string FirstName { get; set; }
       
        
    }
}
