using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoFactorAuth.Models;

namespace TwoFactorAuth.Models
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions <ApplicationDbContext> options)
       : base(options)
        {
        }
        public DbSet<TwoFactorAuth.Models.EnableFactor> EnableFactor { get; set; }

    }
}
