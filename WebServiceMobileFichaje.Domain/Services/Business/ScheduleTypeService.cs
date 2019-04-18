using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WebServiceMobileFichaje.Domain.EF.Model;
using WebServiceMobileFichaje.Domain.Repositories;
using WebServiceMobileFichaje.Domain.Transfer.Business;

namespace WebServiceMobileFichaje.Domain.Services.Business
{
    public class ScheduleTypeService
    {
        private readonly IRepository<TipoDeHorario> _repo;

        public ScheduleTypeService(IRepository<TipoDeHorario> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ScheduleType>> GetAll()
        {
            return await _repo.Query
                .Select(s => new ScheduleType()
                {
                    TipoHorarioID = s.TipoHorarioID,
                    Description = s.Descripcion
                })
                .ToListAsync();
        }
    }
}