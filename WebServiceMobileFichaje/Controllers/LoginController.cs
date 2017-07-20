using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using WebServiceMobileFichaje.Models;
using WebServiceMobileFichaje.Services;
using WebServiceMobileFichaje.Validaciones;

namespace WebServiceMobileFichaje.Controllers
{
    public class LoginController : ApiController
    {
        private readonly TimeSheetUsuarioService _service;

        public LoginController(TimeSheetUsuarioService service)
        {
            _service = service;
        }
       
        [HttpPost]
        public IHttpActionResult Post(LoginUsuario usuario)
        {
            if (usuario == null)
               return BadRequest();
            if (ModelState.IsValid)
               return Ok(_service.AsignarUUID(usuario));
            else
               return BadRequest(ModelState);
        }
    }
}
