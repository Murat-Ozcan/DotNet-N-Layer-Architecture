using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using MRTFramework.CrossCuttingConcern.Utils.Extensions;

namespace MRTFramework.CrossCuttingConcern.Security
{
    public class JsonWebToken : IJsonWebToken
    {
        public TokenValidationParameters TokenValidationParameters => new TokenValidationParameters
        {
            IssuerSigningKey = JsonWebTokenSettings.SecurityKey,
            ValidateActor = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidAudience = JsonWebTokenSettings.Audience,
            ValidIssuer = JsonWebTokenSettings.Issuer,
            ClockSkew = TimeSpan.Zero

        };

        public Dictionary<string, object> Decode(string token)
        {
            return new JwtSecurityTokenHandler().ReadJwtToken(token).Payload;
        }

        public string Encode(string sub, string[] roles)
        {
            var claims = new List<Claim>();
            claims.AddJti();
            claims.AddRoles(roles);
            claims.AddSub(sub);

            return new JwtSecurityTokenHandler().WriteToken(CreateJwtSecurityToken(claims));
        }

        private static JwtSecurityToken CreateJwtSecurityToken(IEnumerable<Claim> claims)
        {
            return new JwtSecurityToken
            (
                JsonWebTokenSettings.Issuer,
                JsonWebTokenSettings.Audience,
                claims,
                DateTime.UtcNow,
                JsonWebTokenSettings.Expires,
                JsonWebTokenSettings.SigningCredentials
            );
        }
    }
}
