using WebAPICRUD_JWT.Models;

namespace WebAPICRUD_JWT.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);  
    }
}
