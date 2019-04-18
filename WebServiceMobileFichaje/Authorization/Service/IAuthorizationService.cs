using System.Net.Http.Headers;
using System.Security.Principal;
using System.Threading.Tasks;

namespace WebServiceMobileFichaje.Authorization.Service
{
    public interface IAuthorizationService
    {
        Task<IPrincipal> GetUserPrincipal(HttpRequestHeaders actionContext);
    }
}
