using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Http;
using WebServiceMobileFichaje.Domain.Services.Business;
using WebServiceMobileFichaje.Domain.Utils;

namespace WebServiceMobileFichaje.Authorization.Service
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly ITokenService _tokenService;
        private readonly TimeSheetUsuarioService _service;

        public AuthorizationService(ITokenService tokenService, TimeSheetUsuarioService service)
        {
            _tokenService = tokenService;
            _service = service;
        }

        public async Task<IPrincipal> GetUserPrincipal(HttpRequestHeaders headers)
        {
            if (!ValidateRequest(headers, out ClaimsIdentity userIdentity))
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized));

            var applicationUser = userIdentity.GetUser();
            if (!applicationUser.IsUserValid())
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized));

            var timeSheetUsuario = await _service.GetFromAppUser(applicationUser);
            if (timeSheetUsuario == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized));

            return _tokenService.GetPrincipal(userIdentity, timeSheetUsuario);
        }

        private bool ValidateRequest(HttpRequestHeaders headers, out ClaimsIdentity identity)
        {
            identity = null;
            return ValidateHeaders(headers) && ValidateToken(headers, out identity);
        }

        private bool ValidateHeaders(HttpRequestHeaders headers)
        {
            var authorization = headers.Authorization;

            if (authorization == null || authorization.Scheme != "Bearer")
                return false;

            if (string.IsNullOrEmpty(authorization.Parameter))
                return false;

            return true;
        }

        private bool ValidateToken(HttpRequestHeaders headers, out ClaimsIdentity identity)
        {
            var token = headers.Authorization.Parameter;
            identity = _tokenService.GetIdentityFromToken(token);

            if (!_tokenService.IsTokenValid(token, identity))
                return false;

            return true;
        }
    }
}