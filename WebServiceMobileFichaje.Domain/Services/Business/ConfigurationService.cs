using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebServiceMobileFichaje.Domain.EF.Model;
using WebServiceMobileFichaje.Domain.Exceptions;
using WebServiceMobileFichaje.Domain.Repositories;
using WebServiceMobileFichaje.Domain.Transfer.Business;

namespace WebServiceMobileFichaje.Domain.Services.Business
{
    public class ConfigurationService
    {
        public static string NoDeviceAssociatedToLocationForGivenTenant = "No existe un dispositivo asociado a ninguna locacion. Contactese con un administrador";
        private readonly IRepository<TimeSheetLocacion> _repo;

        public ConfigurationService(IRepository<TimeSheetLocacion> repo)
        {
            _repo = repo;
        }

        public async Task<MobileConfiguration> GetConfigurationAsync()
        {
            var location = await _repo.Where(l => l.Dispositivos.Count > 0).FirstOrDefaultAsync();

            if (location == null)
                throw new BusinessValidationException(NoDeviceAssociatedToLocationForGivenTenant);

            return new MobileConfiguration()
            {
                ClockInInterval = location.Dispositivos.FirstOrDefault().IntervaloFichado
            };
        }
    }
}