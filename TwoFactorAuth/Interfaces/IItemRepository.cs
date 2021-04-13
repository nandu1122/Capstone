using TwoFactorAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwoFactorAuth.Interfaces
{
    public interface IItemRepository
    {
        public IEnumerable<Product> Items { get; }
        public IEnumerable<Product> Laptops { get; }
        public Product GetItemById(int itemId);
    }
}
