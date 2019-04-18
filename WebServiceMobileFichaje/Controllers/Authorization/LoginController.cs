using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebServiceMobileFichaje.Authorization.Model;
using WebServiceMobileFichaje.Authorization.Service;
using WebServiceMobileFichaje.Domain.Services.Authorization;
using WebServiceMobileFichaje.Domain.Transfer.Authorization;

namespace WebServiceMobileFichaje.Controllers.Authorization
{
    public class LoginController : ApiController
    {
        private readonly LoginService _service;
        private readonly ITokenService _tokenService;

        public LoginController(LoginService service, ITokenService tokenService)
        {
            _service = service;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [AcceptVerbs("POST")]
        [HttpPost]
        [ResponseType(typeof(JWT))]
        public async Task<IHttpActionResult> Post(LoginCredentials credentials)
        {
            var validationResult = await _service.ValidateCredentials(credentials);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.ErrorMsg);

            var timesheetUsuario = await _service.GenerateUUID(credentials);

            var token = _tokenService.GenerateToken(timesheetUsuario.TimeSheetUsuarioID, timesheetUsuario.UUID);

            return Ok(token);
        }
    }
}
