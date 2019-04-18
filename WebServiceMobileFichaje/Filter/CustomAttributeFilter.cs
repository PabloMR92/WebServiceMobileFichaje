using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebServiceMobileFichaje.Filter
{
    public abstract class CustomAttributeFilter<TAttribute> : IActionFilter where TAttribute : Attribute
    {
        private TAttribute _attribute;
        public TAttribute Attribute => _attribute;

        public Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            var controllerHasAttribute = ControllerHasAttribute(actionContext);
            var actionHasAttribute = ActionHasAttribute(actionContext);
            if (controllerHasAttribute || actionHasAttribute)
            {
                _attribute = controllerHasAttribute ? AttributeFromController(actionContext) : AttributeFromAction(actionContext);
                return ExecuteFilterBehavior(actionContext, cancellationToken, continuation);
            }
            return continuation();
        }

        protected abstract Task<HttpResponseMessage> ExecuteFilterBehavior(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation);

        public bool AllowMultiple => true;

        private bool ControllerHasAttribute(HttpActionContext actionContext)
        {
            return actionContext
                    .ControllerContext.Controller.GetType()
                    .GetCustomAttributes(false)
                    .Any(attribute => attribute.GetType().IsAssignableFrom(typeof(TAttribute)));
        }

        private bool ActionHasAttribute(HttpActionContext actionContext)
        {
            return actionContext
                .ActionDescriptor
                .GetCustomAttributes<TAttribute>()
                .Any();
        }

        private TAttribute AttributeFromAction(HttpActionContext actionContext)
        {
            return actionContext
                        .ActionDescriptor
                        .GetCustomAttributes<TAttribute>()
                        .SingleOrDefault();
        }

        private TAttribute AttributeFromController(HttpActionContext actionContext)
        {
            return (TAttribute)actionContext
                    .ControllerContext.Controller.GetType()
                    .GetCustomAttributes(false)
                    .SingleOrDefault(attribute => attribute.GetType().IsAssignableFrom(typeof(TAttribute)));
        }
    }
}