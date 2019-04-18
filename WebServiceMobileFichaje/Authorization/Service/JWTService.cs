using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Web;
using WebServiceMobileFichaje.Authorization.Model;
using System.IdentityModel.Tokens.Jwt;
using WebServiceMobileFichaje.Authorization.Utils;
using WebServiceMobileFichaje.Domain.Transfer.Authorization;
using System.Security.Principal;
using WebServiceMobileFichaje.Domain.Utils;
using WebServiceMobileFichaje.Domain.EF.Model;

namespace WebServiceMobileFichaje.Authorization.Service
{
    public class JWTService : ITokenService
    {
        public JWT GenerateToken(int userId, string uuid)
        {
            RsaSecurityKey rsaKey = CertificateUtils.GetRSAKey();
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                        {
                            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                            new Claim(ClaimTypes.MobilePhone, uuid)
                        }),
                SigningCredentials = new SigningCredentials(rsaKey, SecurityAlgorithms.RsaSha512Signature)
            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);

            return new JWT() { Token = token };
        }

        public ClaimsIdentity GetIdentityFromToken(string token)
        {
            var simplePrinciple = GetPrincipal(token);
            var identity = simplePrinciple?.Identity as ClaimsIdentity;
            return identity;
        }

        public ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                    return null;

                RsaSecurityKey rsaKey = CertificateUtils.GetRSAKey();

                var validationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = rsaKey,
                    ValidateLifetime = false
                };
                
                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken securityToken);
                return principal;
            }

            catch (Exception)
            {
                return null;
            }
        }

        public IPrincipal GetPrincipal(ClaimsIdentity identity, TimeSheetUsuario user)
        {
            identity.AddClaim(new Claim(ClaimsUtils.Company, user.GrupoID.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.LogIn));
            IPrincipal principal = new ClaimsPrincipal(identity);
            return principal;
        }

        public bool IsTokenValid(string token, ClaimsIdentity identity)
        {
            var username = String.Empty;

            if (identity == null)
                return false;

            if (!identity.IsAuthenticated)
                return false;

            var usernameClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
            var uuidClaim = identity.FindFirst(ClaimTypes.MobilePhone);

            return !string.IsNullOrEmpty(usernameClaim?.Value) && !string.IsNullOrEmpty(uuidClaim?.Value);
        }
    }
}