using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebServiceMobileFichaje.Services;
using WebServiceMobileFichaje.ViewModels;

namespace WebServiceMobileFichaje.Controllers
{
    public class EstablecimientoController : ApiController
    {
        private readonly TimeSheetLocationService _service;

        public EstablecimientoController(TimeSheetLocationService service)
        {
            _service = service;
        }
        
        [Authorize]
        public IEnumerable<TimeSheetLocationListItemViewModel> Get()
        {
            var reporte = _service.GetLocationsItemList();
            return reporte;
        }

    }
}
