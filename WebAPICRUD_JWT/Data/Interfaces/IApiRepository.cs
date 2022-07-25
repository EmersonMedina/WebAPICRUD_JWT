using WebAPICRUD_JWT.Models;

namespace WebAPICRUD_JWT.Data.Interfaces
{
    public interface IApiRepository
    {
        void Add<T> (T entity) where T:class;
        void Delete<T> (T entity) where T:class;
        Task<bool> SaveAll();
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByNameAsync(string name);
        Task<IEnumerable<Product>> GetProductsAsync(); 
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> GetProductByNameAsync(string name); 
    }
}
