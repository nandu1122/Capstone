using TwoFactorAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwoFactorAuth.ViewModel
{
    public class CellPhoneViewModel
    {
        public IEnumerable<CellPhone> CellPhones { get; set; }
        public string CurrentCategory { get; set; }
    }
}
