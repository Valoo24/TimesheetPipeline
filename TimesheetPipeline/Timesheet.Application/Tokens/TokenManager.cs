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
            _issuer = config.GetSection("TokenInfo").GetSection("issuer").Value;
            _audience = config.GetSection("TokenInfo").GetSection("audience").Value;
            _secret = config.GetSection("TokenInfo").GetSection("secret").Value;
        }

        public string GenerateToken(User user)
        {
            if (user is null) throw new ArgumentNullException("Le user ne peut être null pour la création d'un token.");

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

            //Remplir ici les claims pour l'entity user.
            Claim[] claims = new Claim[]
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Email, user.MailAdress),
                new Claim(ClaimTypes.Hash, user.HashedPassword),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

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