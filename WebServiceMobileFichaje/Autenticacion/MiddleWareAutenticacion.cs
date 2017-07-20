using Microsoft.Owin;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Runtime.Caching;
using WebServiceMobileFichaje.Validaciones;
using WebServiceMobileFichaje.Repository;
using WebServiceMobileFichaje.Models;
using WebServiceMobileFichaje.Context;
using System.IO;

namespace WebServiceMobileFichaje.Autenticacion
{
    public class MiddleWareAutenticacion : OwinMiddleware
    {
        private readonly IValidacionUUID _validator;
        
        public MiddleWareAutenticacion(OwinMiddleware next, IValidacionUUID validator) :
                                                   base(next)
        { _validator = validator; }

        public override async Task Invoke(IOwinContext context)
        {
            var response = context.Response;
            var request = context.Request;

            var UUID = request.Query.Get("UUID");
            
            if (_validator.UUIDEsValido(UUID)) 
            {
                var claims = new[] { new Claim(ClaimTypes.Name, UUID) };
                var identity = new ClaimsIdentity(claims, "Basic");
                request.User = new ClaimsPrincipal(identity);
            }

            await Next.Invoke(context);
        }
    }
}