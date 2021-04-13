using TwoFactorAuth.Interfaces;
using TwoFactorAuth.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwoFactorAuth.Repositories
{
    public class ICellPhoneRepository : ICellPhoneInterface
    {
        private readonly AppDbContext _appDbContext;
        public ICellPhoneRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

        }
        public IEnumerable<CellPhone> Items => _appDbContext.CellPhones.Include(c => c.Category);

        public IEnumerable<CellPhone> Laptops => _appDbContext.CellPhones.Where(p => p.InStock).Include(c => c.Category);



        public CellPhone GetItemById(int itemId)
        => _appDbContext.CellPhones.FirstOrDefault(p => p.CellPhoneId == itemId);

        
    }
}
