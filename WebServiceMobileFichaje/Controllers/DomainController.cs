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
    public class DomainController : ApiController
    {
        private readonly TimeSheetDomainService _service;

        public DomainController(TimeSheetDomainService service)
        {
            _service = service;
        }
        
        public IEnumerable<TimeSheetDomain> Get()
        {
            var reporte = _service.GetDomainsItemList();
            return reporte;
        }
    }
}
