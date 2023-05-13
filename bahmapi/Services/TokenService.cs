using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using bahmapi.Entities;
using Microsoft.IdentityModel.Tokens;

namespace bahmapi.Services
{
    public class TokenService
    {
        public string GenerateToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Secret.SecretString);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.IdUsuario.ToString()),
                    new Claim(ClaimTypes.Role, usuario.PerfilUsuario)
                    // new Claim("ID", usuario.IdUsuario.ToString())
                    
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}