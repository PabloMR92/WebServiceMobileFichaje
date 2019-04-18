using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebServiceMobileFichaje.Authorization;
using WebServiceMobileFichaje.Domain.Services.Business;
using WebServiceMobileFichaje.Domain.Transfer.Business;

namespace WebServiceMobileFichaje.Controllers.Business
{
    public class ScheduleTypeController : ApiController
    {
        private readonly ScheduleTypeService _service;

        public ScheduleTypeController(ScheduleTypeService service)
        {
            _service = service;
        }

        [JWTAuthorize]
        [AcceptVerbs("GET")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<ScheduleType>))]
        public async Task<IHttpActionResult> Get()
        {
            var result = await _service.GetAll();
            return Ok(result);
        }
    }
}
