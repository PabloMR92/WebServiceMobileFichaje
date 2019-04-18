using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebServiceMobileFichaje.Authorization.Service;
using WebServiceMobileFichaje.Models;

namespace WebServiceMobileFichaje.Controllers.Authorization
{
    public class TokenInfoController : ApiController
    {
        private readonly IAuthorizationService _service;

        public TokenInfoController(IAuthorizationService service)
        {
            _service = service;
        }

        [AcceptVerbs("GET")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<TokenInfo>))]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                await _service.GetUserPrincipal(Request.Headers);
                return Ok(new TokenInfo() { IsValid = true });
            }
            catch
            {
                return Ok(new TokenInfo() { IsValid = false });
            }
        }
    }
}
