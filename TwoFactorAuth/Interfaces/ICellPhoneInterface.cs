using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoFactorAuth.Models;

namespace TwoFactorAuth.Interfaces
{
    public interface ICellPhoneInterface
    {
        public IEnumerable<CellPhone> Items { get; }
        public IEnumerable<CellPhone> Laptops { get; }
        public CellPhone GetItemById(int itemId);
    }
}
