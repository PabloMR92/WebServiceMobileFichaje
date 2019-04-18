using System;
using System.Security.Claims;
using WebServiceMobileFichaje.Domain.Transfer.Authorization;

namespace WebServiceMobileFichaje.Domain.Utils
{

    public static class ClaimsUtils
    {
        public const string Company = "Company";
        public static ApplicationUser GetUser(this ClaimsIdentity user)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
            var uuidClaim = user.FindFirst(ClaimTypes.MobilePhone);
            var logInClaim = user.FindFirst(ClaimTypes.Name);
            var companyClaim = user.FindFirst(Company);

            return new ApplicationUser()
            {
                UserId = userIdClaim != null ? Int32.Parse(userIdClaim.Value) : 0,
                GrupoId = companyClaim != null ? Int32.Parse(companyClaim.Value) : 0,
                LogIn = logInClaim?.Value,
                UUID = uuidClaim?.Value
            };
        }
    }
}