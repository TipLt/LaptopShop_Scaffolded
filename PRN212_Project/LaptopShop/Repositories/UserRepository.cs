using LaptopShop.Model;
using System.Linq;

namespace LaptopShop.Repositories
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(LaptopShopDbContext context) : base(context)
        {
        }

        public User? GetByUsername(string username)
        {
            return _dbSet.FirstOrDefault(u => u.Username == username);
        }

        public User? Authenticate(string username, string password)
        {
            return _dbSet.FirstOrDefault(u => 
                u.Username == username && 
                u.Password == password && 
                u.IsActive == true);
        }
    }
}
