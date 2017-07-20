using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Data.Entity;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using WebServiceMobileFichaje.ViewModels;
using WebServiceMobileFichaje.Models;
using WebServiceMobileFichaje.Repository;
using WebServiceMobileFichaje.Services;
using WebServiceMobileFichaje.Context;
using System.Threading.Tasks;

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
