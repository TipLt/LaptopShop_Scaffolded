using LaptopShop.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LaptopShop.Repositories
{
    public class OrderRepository : Repository<Order>
    {
        public OrderRepository(LaptopShopDbContext context) : base(context)
        {
        }

        public IEnumerable<Order> GetAllWithDetails()
        {
            return _dbSet
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Laptop)
                .ToList();
        }

        public Order? GetByIdWithDetails(int id)
        {
            return _dbSet
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Laptop)
                .FirstOrDefault(o => o.OrderId == id);
        }
    }
}
