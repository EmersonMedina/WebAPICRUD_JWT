using WebAPICRUD_JWT.Models;

namespace WebAPICRUD_JWT.Data.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password); 
        Task<User> Login(string email, string password);
        Task<bool> ExistsUser(string email);

    }
}
