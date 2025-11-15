using LaptopShop.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LaptopShop.Repositories
{
    public class LaptopRepository : Repository<Laptop>
    {
        public LaptopRepository(LaptopShopDbContext context) : base(context)
        {
        }

        public IEnumerable<Laptop> GetAllWithDetails()
        {
            return _dbSet
                .Include(l => l.Categories)
                .Include(l => l.LaptopSuppliers)
                    .ThenInclude(ls => ls.Supplier)
                .ToList();
        }

        public Laptop? GetByIdWithDetails(int id)
        {
            return _dbSet
                .Include(l => l.Categories)
                .Include(l => l.LaptopSuppliers)
                    .ThenInclude(ls => ls.Supplier)
                .FirstOrDefault(l => l.LaptopId == id);
        }
    }
}
