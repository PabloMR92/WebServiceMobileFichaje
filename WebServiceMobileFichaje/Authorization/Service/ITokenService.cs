using System.Security.Claims;
using System.Security.Principal;
using WebServiceMobileFichaje.Authorization.Model;
using WebServiceMobileFichaje.Domain.EF.Model;

namespace WebServiceMobileFichaje.Authorization.Service
{
    public interface ITokenService
    {
        bool IsTokenValid(string token, ClaimsIdentity identity);
        ClaimsIdentity GetIdentityFromToken(string token);
        JWT GenerateToken(int userId, string uuid);
        ClaimsPrincipal GetPrincipal(string token);
        IPrincipal GetPrincipal(ClaimsIdentity identity, TimeSheetUsuario user);
    }
}
