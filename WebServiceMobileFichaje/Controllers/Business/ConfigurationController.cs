using System.Threading.Tasks;
using System.Web.Http;
using WebServiceMobileFichaje.Authorization;
using WebServiceMobileFichaje.Domain.Services.Business;

namespace WebServiceMobileFichaje.Controllers.Business
{
    public class ConfigurationController : ApiController
    {
        private readonly ConfigurationService _configurationService;

        public ConfigurationController(ConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        [JWTAuthorize]
        [AcceptVerbs("Get")]
        [HttpPost]
        public async Task<IHttpActionResult> Get()
        {
           var configuration = await _configurationService.GetConfigurationAsync();
           return Ok(configuration);
        }
    }
}
