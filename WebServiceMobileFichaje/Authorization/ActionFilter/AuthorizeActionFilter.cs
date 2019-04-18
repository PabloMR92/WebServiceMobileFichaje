using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using WebServiceMobileFichaje.Authorization.Service;
using WebServiceMobileFichaje.Filter;

namespace WebServiceMobileFichaje.Authorization.ActionFilter
{
    public class AuthorizeActionFilter : CustomAttributeFilter<JWTAuthorizeAttribute>
    {
        private readonly IAuthorizationService _service;

        public AuthorizeActionFilter(IAuthorizationService service)
        {
            _service = service;
        }

        protected override async Task<HttpResponseMessage> ExecuteFilterBehavior(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            var principal = await _service.GetUserPrincipal(actionContext.Request.Headers);
            actionContext.RequestContext.Principal = principal;

            return await continuation();
        }        
    }
}