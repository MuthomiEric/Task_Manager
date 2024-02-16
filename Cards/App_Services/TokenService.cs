using System.Text;
using Core.Interfaces;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Core.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace Cards.App_Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;

        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
            _config = config;

            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]));
        }

        public (string expiresin, string token) CreateToken(SystemUser user, IList<string>? roles = null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!),

                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            if (roles is not null)
            {
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            // Hours
            var expiresIn = 1;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(expiresIn),
                SigningCredentials = creds,
                Issuer = _config["Token:Issuer"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return ($"{expiresIn}h", tokenHandler.WriteToken(token));
        }
    }
}
