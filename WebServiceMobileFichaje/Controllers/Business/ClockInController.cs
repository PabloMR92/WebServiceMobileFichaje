using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebServiceMobileFichaje.Authorization;
using WebServiceMobileFichaje.Domain.Services.Business;
using WebServiceMobileFichaje.Domain.Transfer.Business;

namespace WebServiceMobileFichaje.Controllers.Business
{
    public class ClockInController : ApiController
    {
        private readonly TimeSheetTemporalService _timeSheetTempService;

        public ClockInController(TimeSheetTemporalService timeSheetTempService)
        {
            _timeSheetTempService = timeSheetTempService;
        }

        [JWTAuthorize]
        [AcceptVerbs("POST")]
        [HttpPost]
        public async Task<IHttpActionResult> Post(CurrentLocation currentLocation)
        {
            await _timeSheetTempService.ClockIn(currentLocation);
            return Ok();
        }
    }
}
