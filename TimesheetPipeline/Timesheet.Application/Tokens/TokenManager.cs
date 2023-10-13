using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Timesheet.Domain.Entities.Users;
using Timesheet.Domain.Interfaces;

namespace Timesheet.Application.Tokens
{
    public class TokenManager : ITokenManager
    {
        private readonly string _issuer;
        private readonly string _audience;
        private readonly string _secret;

        public TokenManager(IConfiguration config)
        {
            
        }

        public string GenerateToken(User user)
        {
            if (user is null) throw new ArgumentNullException("Le user ne peut être null pour la création d'un token.");

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

            //Remplir ici les claims pour l'entity user.
            Claim[] claims = new Claim[] { };

            //Configuration du Token
            JwtSecurityToken token = new JwtSecurityToken(
                claims : claims,
                signingCredentials: credentials,
                issuer: _issuer,
                audience: _audience,
                expires: DateTime.Now.AddDays(1)
                );

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(token);
        }
    }
}