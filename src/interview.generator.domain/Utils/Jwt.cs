using interview.generator.domain.Entidade;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace interview.generator.domain.Utils
{
    public static class Jwt
    {

        public static string GeraToken(Usuario usuario, DateTime validade, IConfiguration configuration)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Secret:Key"]!);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.Nome),
                    new Claim(ClaimTypes.Role, usuario.Perfil.ToString()),
                    new Claim("Id", usuario.Id.ToString())

                }),

                Expires = validade,
                SigningCredentials = new SigningCredentials(
                             new SymmetricSecurityKey(key),
                             SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
