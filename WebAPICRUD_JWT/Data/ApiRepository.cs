using Microsoft.EntityFrameworkCore;
using WebAPICRUD_JWT.Data.Interfaces;
using WebAPICRUD_JWT.Models;

namespace WebAPICRUD_JWT.Data
{
    public class ApiRepository : IApiRepository
    {
        private readonly DataContext _context;
        public ApiRepository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity); 
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            IEnumerable<Product> Products = await _context.Products.ToListAsync();
            return Products; 
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            Product product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            return product; 
        }

        public async Task<Product> GetProductByNameAsync(string name)
        {
            Product product = await _context.Products.FirstOrDefaultAsync(p => p.Name == name);
            return product;
        }


        public async Task<User> GetUserByIdAsync(int id)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            return user; 
        }

        public async Task<User> GetUserByNameAsync(string name)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Name == name);
            return user;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            IEnumerable<User> users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;  
        }
    }
}
