using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwoFactorAuth.Models
{
    public class ShoppingCartItem
    {
        public int ShoppingCartItemId { get; set; }
        public Product Item { get; set; }
        public CellPhone CellPhone { get; set; }
        public decimal Amount { get; set; }
        public string ShoppingCartId { get; set; }
    }
}
