using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebServiceMobileFichaje.Authorization;
using WebServiceMobileFichaje.Domain.Services.Business;
using WebServiceMobileFichaje.Domain.Transfer.Business;

namespace WebServiceMobileFichaje.Controllers.Business
{
    public class ReportController : ApiController
    {
        private readonly ReportService _service;

        public ReportController(ReportService service)
        {
            _service = service;
        }

        [JWTAuthorize]
        [AcceptVerbs("GET")]
        [HttpGet]
        [ResponseType(typeof(InOutUserReportResult))]
        public async Task<IHttpActionResult> Get([FromUri] InOutUserReportRequest request)
        {
            var report = await _service.Get(request);
            return Ok(report);
        }
    }
}
