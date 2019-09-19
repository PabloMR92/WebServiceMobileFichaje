using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebServiceMobileFichaje.Authorization.Service;
using WebServiceMobileFichaje.Domain.Services.Authorization;
using WebServiceMobileFichaje.Domain.Transfer.Authorization;
using WebServiceMobileFichaje.Models;

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
        [ResponseType(typeof(WebServiceMobileFichaje.Authorization.Model.JWT))]
        public async Task<IHttpActionResult> Post(LoginCredentials credentials)
        {
            var validationResult = await _service.ValidateCredentials(credentials);
            if (!validationResult.IsValid)
                return Content(HttpStatusCode.BadRequest, new ErrorHttpActionResult
                {
                    ErrorMsg = validationResult.ErrorMsg
                });

            var timesheetUsuario = await _service.GenerateUUID(credentials);

            var token = _tokenService.GenerateToken(timesheetUsuario.TimeSheetUsuarioID, timesheetUsuario.UUID);

            return Ok(token);
        }
    }
}
