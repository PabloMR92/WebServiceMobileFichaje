using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using WebServiceMobileFichaje.Autenticacion;
using System.Web.Http;
using Unity.WebApi;
using WebServiceMobileFichaje.Validaciones;
using Microsoft.Practices.Unity;

[assembly: OwinStartup(typeof(WebServiceMobileFichaje.Startup1))]

namespace WebServiceMobileFichaje
{
    public class Startup1
    {
        public void Configuration(IAppBuilder app)
        {
            IValidacionUUID theDataService = UnityConfig.container.Resolve<IValidacionUUID>();
            
            app.Use<MiddleWareAutenticacion>(theDataService);
            // Para obtener más información acerca de cómo configurar su aplicación, visite http://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
