using TwoFactorAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwoFactorAuth.ViewModel
{
    public class HomeViewModel
    {
        public IEnumerable<Product> InStock { get; set; }
    }
}
