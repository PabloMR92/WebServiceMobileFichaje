using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebServiceMobileFichaje.Services;

namespace WebServiceMobileFichaje.Controllers
{
    public class UUIDController : ApiController
    {
        private readonly TimeSheetUsuarioService _service;

        public UUIDController(TimeSheetUsuarioService service)
        {
            _service = service;
        }
        
        public bool Get([FromUri] string UUID)
        {
            var result = _service.ExisteUUID(UUID);
            return result;
        }
    }
}
