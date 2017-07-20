using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Practices.Unity;
using WebServiceMobileFichaje.Models;
using WebServiceMobileFichaje.Repository;
using WebServiceMobileFichaje.Services;
using WebServiceMobileFichaje.Validaciones;

namespace WebServiceMobileFichaje.Controllers
{
    public class GeoLocationController : ApiController
    {
        [HttpPost]
        [Authorize]
        public IHttpActionResult Post([FromBody]GeoLocationTimeSheet actualLocation)
        {
            if (ModelState.IsValid)
            {
                ValidacionGeoLocationProximidad validator = new ValidacionGeoLocationProximidad();
                var repo = UnityConfig.container.Resolve<IRepository<TimeSheetLocacion>>();
                var userRepo = UnityConfig.container.Resolve<IRepository<TimeSheetUsuario>>();
                var usuario = userRepo.Query.Where(x => x.UUID == actualLocation.UUID).FirstOrDefault();
                var service = new TimeSheetLocationService(repo);
                var establecimientos = service.GetLocations();
                var establecimiento = validator.getEstablecimientoProximo(establecimientos, actualLocation);
                var repoTimeSheetTemp = UnityConfig.container.Resolve<IRepository<TimeSheetTemporal>>();
                repoTimeSheetTemp.Agregar(new TimeSheetTemporal()
                {
                    LogIn = usuario.LogIn,
                    GrupoID = usuario.GrupoID,
                    TimeSheetLocationID = establecimiento.TimeSheetLocationID,
                    NumeroDispositivo = 3,
                    Fecha = actualLocation.Timestamp.ToString()
                });
                repoTimeSheetTemp.Save();
                return Ok(new GeoLocationResponse() { Mensaje = "Fichaje correcto" });
            }
            else
                return BadRequest(ModelState);
        }
    }
}
