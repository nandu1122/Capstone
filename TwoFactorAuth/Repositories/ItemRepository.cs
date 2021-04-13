using TwoFactorAuth.Interfaces;
using TwoFactorAuth.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwoFactorAuth.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly AppDbContext _appDbContext;
        public ItemRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

        }
        public IEnumerable<Product> Items => _appDbContext.Items.Include(c => c.Category);

        public IEnumerable<Product> Laptops => _appDbContext.Items.Where(p => p.InStock).Include(c => c.Category);



        public Product GetItemById(int itemId)
        => _appDbContext.Items.FirstOrDefault(p => p.ItemId == itemId);
    }
}


