using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using WebServiceMobileFichaje.Domain.Exceptions;
using WebServiceMobileFichaje.Models;

namespace WebServiceMobileFichaje.Filter
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is BusinessValidationException exception)
                context.Response = context.Request.CreateResponse(HttpStatusCode.BadRequest, new ErrorHttpActionResult
                {
                    ErrorMsg = exception.Message
                });
        }
    }
}