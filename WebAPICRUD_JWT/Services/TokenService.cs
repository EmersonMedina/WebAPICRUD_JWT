using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPICRUD_JWT.Models;
using WebAPICRUD_JWT.Services.Interfaces;

namespace WebAPICRUD_JWT.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _sskey; 
        public TokenService(IConfiguration config)
        {
            _sskey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token"])); 
        }
        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Email)
            }; 

            var credentials = new SigningCredentials(_sskey, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token); 
        }
    }
}
