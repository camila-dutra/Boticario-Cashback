using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using Cashback.Auth.Models;
using Cashback.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Cashback.Auth.Services
{
    public class TokenService
    {
        public static string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
                                  {
                                      Subject = new ClaimsIdentity(new Claim[]
                                                                   {
                                                                       new Claim(ClaimTypes.Name, user.Name),
                                                                       new Claim(ClaimTypes.Email, user.Email),
                                                                       new Claim(ClaimTypes.NameIdentifier, user.Cpf.ToString())
                                                                   }),
                                      Expires = DateTime.UtcNow.AddHours(3),
                                      SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                                  };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static string GetValueFromClaim(IIdentity identity, string field)
        {
            var claims = identity as ClaimsIdentity;

            return claims.FindFirst(field).Value;
        }

    }
}
