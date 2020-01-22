using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Account.Application.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Account.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly TokenConfiguration _tokenConfiguration;

        public TokenService(IOptions<TokenConfiguration> tokenConfigurationOptions)
        {
            _tokenConfiguration = tokenConfigurationOptions.Value;
        }

        public string Generate(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = GetDescriptor(email);
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private SecurityTokenDescriptor GetDescriptor(string email)
        {
            var key = Encoding.ASCII.GetBytes(_tokenConfiguration.Secret);

            return new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email)
                }),
                Expires = DateTime.Now.AddSeconds(_tokenConfiguration.Expiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
        }
    }
}