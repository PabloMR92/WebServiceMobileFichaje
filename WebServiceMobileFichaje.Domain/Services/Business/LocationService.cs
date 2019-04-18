using MoreLinq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Device.Location;
using System.Linq;
using System.Threading.Tasks;
using WebServiceMobileFichaje.Domain.EF.Model;
using WebServiceMobileFichaje.Domain.Repositories;
using WebServiceMobileFichaje.Domain.Transfer.Business;
using WebServiceMobileFichaje.Domain.Transfer.Validation;

namespace WebServiceMobileFichaje.Domain.Services.Business
{
    public class LocationService
    {
        public static string NearLocationNotFound = "No se encuentra dentro del radio de proximidad establecido para el establecimiento";

        private readonly IRepository<TimeSheetLocacion> _repo;

        public LocationService(IRepository<TimeSheetLocacion> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Location>> GetAll()
        {
            return await _repo.Query
                .Where(x => x.CoordenadaX != null && x.CoordenadaY != null && x.RadioPermitido != null)
                .Select(x => new Location()
                {
                    CoordenadaX = x.CoordenadaX,
                    CoordenadaY = x.CoordenadaY,
                    TimeSheetLocationID = x.TimeSheetLocacionID,
                    RadioPermitido = x.RadioPermitido,
                    Descripcion = x.Descripcion
                }).ToListAsync();
        }

        public async Task<Location> GetNearestLocation(CurrentLocation currentLocation)
        {
            var result = new ValidationResult() { IsValid = true };
            var locations = await GetAll();
            foreach (var location in locations)
                AssignDistanceFrom(from: location, to: currentLocation);

            return locations
                .Where(l => l.Distancia <= (double)l.RadioPermitido)
                .DefaultIfEmpty()
                .MinBy(y => y?.Distancia);
        }

        private void AssignDistanceFrom(Location from, CurrentLocation to)
        {
            var fromCoordinates = new GeoCoordinate((double)from.CoordenadaX, (double)from.CoordenadaY);
            var toCoordinates = new GeoCoordinate((double)to.CoordenadaX, (double)to.CoordenadaY);
            from.Distancia = fromCoordinates.GetDistanceTo(toCoordinates);
        }
    }
}