using System.Collections.Generic;
using System.Web.Http;
using WebServiceMobileFichaje.Models;
using WebServiceMobileFichaje.Services;
using WebServiceMobileFichaje.ViewModels;

namespace WebServiceMobileFichaje.Controllers
{
    public class ListadoTimeSheetController : ApiController
    {
        private readonly TimeSheetViewModelService _service;

        public ListadoTimeSheetController(TimeSheetViewModelService service)
        {
            _service = service;
        }

        // GET: api/ListadoTimeSheet
        [Authorize]
        public IEnumerable<ReporteIngresoEgresoViewModel> Get([FromUri] TimeSheetListadoRequest request)
        {
            var reporte = _service.ReporteXFechas(request);
            return reporte;
        }
        
    }
}
