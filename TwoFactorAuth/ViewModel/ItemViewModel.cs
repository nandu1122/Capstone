using TwoFactorAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwoFactorAuth.ViewModel
{
    public class ItemViewModel
    {
        public IEnumerable<Product> Items { get; set; }
        public string CurrentCategory { get; set; }
    }
}


