using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebServiceMobileFichaje.Models;
using WebServiceMobileFichaje.Repository;
using WebServiceMobileFichaje.Services;
using Microsoft.Practices.Unity;
using System.Device.Location;
using MoreLinq;
using WebServiceMobileFichaje.ViewModels;

namespace WebServiceMobileFichaje.Validaciones
{
    public class ValidacionGeoLocationProximidad : PropertyValidator
    {
        public ValidacionGeoLocationProximidad()
        : base("No se encuentra dentro del radio de proximidad establecido para el establecimiento") { }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var location = context.PropertyValue as GeoLocationTimeSheet;
            var repo = UnityConfig.container.Resolve<IRepository<TimeSheetLocacion>>();
            var service = new TimeSheetLocationService(repo);
            var establecimientos = service.GetLocations();
            return getEstablecimientoProximo(establecimientos, location) != null;
        }

        public TimeSheetLocationViewModel getEstablecimientoProximo(List<TimeSheetLocationViewModel> establecimientos, GeoLocationTimeSheet location)
        {
            TimeSheetLocationViewModel establecimientoProximo;
            if (establecimientos.Count > 0)
            {
                establecimientoProximo = establecimientos
                                        .Where(x => esProximo(x, location))
                                        .DefaultIfEmpty(new TimeSheetLocationViewModel() {TimeSheetLocationID = -1 , Distancia = -1})
                                        .MinBy(y => y.Distancia);
                return establecimientoProximo.TimeSheetLocationID != -1 ? establecimientoProximo : null;
            }
            else
                return null;
        }

        public bool esProximo(TimeSheetLocationViewModel establecimiento, GeoLocationTimeSheet location)
        {
            if (establecimiento.CoordenadaX == null || establecimiento.CoordenadaY == null)
                return false;
            var ubicacionEstablecimiento = new GeoCoordinate((double)establecimiento.CoordenadaX, (double)establecimiento.CoordenadaY);
            var ubicacionUsuario = new GeoCoordinate((double)location.CoordenadaX, (double)location.CoordenadaY);
            var distancia = (double)ubicacionUsuario.GetDistanceTo(ubicacionEstablecimiento);
            establecimiento.Distancia = distancia;
            if ((double)ubicacionUsuario.GetDistanceTo(ubicacionEstablecimiento) > (double)establecimiento.RadioPermitido)
                return false;
            else
                return true;
        }
    }
}