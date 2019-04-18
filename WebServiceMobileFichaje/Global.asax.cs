using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using WebServiceMobileFichaje.Authorization;
using WebServiceMobileFichaje.Authorization.ActionFilter;
using WebServiceMobileFichaje.Authorization.Service;
using WebServiceMobileFichaje.Domain.Services.Business;

namespace WebServiceMobileFichaje
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            /*var kernel = DependencyResolver.Current;
            GlobalConfiguration.Configuration.Filters.Add(new AuthorizeActionFilter(kernel.GetService<ITokenService>(), kernel.GetService<TimeSheetUsuarioService>()));*/
        }
    }
}
