using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WebServiceMobileFichaje.Domain.EF.Model;
using WebServiceMobileFichaje.Domain.Repositories;
using WebServiceMobileFichaje.Domain.Transfer.Authorization;

namespace WebServiceMobileFichaje.Domain.Services.Business
{
    public class TimeSheetUsuarioService
    {
        private readonly IRepository<TimeSheetUsuario> _repo;

        public TimeSheetUsuarioService(IRepository<TimeSheetUsuario> repo)
        {
            _repo = repo;
        }

        public async Task<TimeSheetUsuario> GetFromAppUser(ApplicationUser user)
        {
            return await _repo
                .Where(x => x.UUID == user.UUID && x.TimeSheetUsuarioID == user.UserId)
                .FirstOrDefaultAsync();
        }
    }
}