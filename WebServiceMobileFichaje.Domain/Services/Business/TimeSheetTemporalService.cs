using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebServiceMobileFichaje.Domain.EF.Model;
using WebServiceMobileFichaje.Domain.Exceptions;
using WebServiceMobileFichaje.Domain.Repositories;
using WebServiceMobileFichaje.Domain.Transfer.Authorization;
using WebServiceMobileFichaje.Domain.Transfer.Business;

namespace WebServiceMobileFichaje.Domain.Services.Business
{
    public class TimeSheetTemporalService
    {
        private const int MobileDeviceNumber = 3;
        private readonly LocationService _locationService;
        private readonly IRepository<TimeSheetTemporal> _repo;

        public TimeSheetTemporalService(LocationService locationService, IRepository<TimeSheetTemporal> repo)
        {
            _locationService = locationService;
            _repo = repo;
        }

        public async Task ClockIn(CurrentLocation currentLocation)
        {
            var nearestLocation = await _locationService.GetNearestLocation(currentLocation);

            if (nearestLocation == null)
                throw new BusinessValidationException(LocationService.NearLocationNotFound);

            var currentUser = ApplicationUser.GetCurrent();
            _repo.Add(new TimeSheetTemporal()
             {
                 LogIn = currentUser.LogIn,
                 GrupoID = currentUser.GrupoId,
                 TimeSheetLocacionID = nearestLocation.TimeSheetLocationID,
                 NumeroDispositivo = MobileDeviceNumber,
                 Fecha = currentLocation.Timestamp.ToString()
             });

            await _repo.SaveAsync();
        }
    }
}