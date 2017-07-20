using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebServiceMobileFichaje.Models;
using WebServiceMobileFichaje.Services;

namespace WebServiceMobileFichaje.Controllers
{
    public class TipoDeHorarioController : ApiController
    {
        private readonly TipoDeHorarioService _service;

        public TipoDeHorarioController(TipoDeHorarioService service)
        {
            _service = service;
        }

        [Authorize]
        public IEnumerable<TipoDeHorario> Get()
        {
            var reporte = _service.GetTipoDeHorarioItemList();
            return reporte;
        }
    }
}
