using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebServiceMobileFichaje.ViewModels;
using WebServiceMobileFichaje.Models;
using WebServiceMobileFichaje.Repository;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Data.Entity;
using WebServiceMobileFichaje.ExtensionMethods;

namespace WebServiceMobileFichaje.Services
{
    public class TimeSheetViewModelService
    {
        private readonly IRepository<TimeSheetViewModel> _repoViewModel;
        private readonly IRepository<TimeSheetUsuario> _repoUsuario;

        public TimeSheetViewModelService(IRepository<TimeSheetViewModel> repoViewModel, IRepository<TimeSheetUsuario> repoUsuario)
        {
            this._repoViewModel = repoViewModel;
            this._repoUsuario = repoUsuario;
        }
        
        public List<ReporteIngresoEgresoViewModel> ReporteXFechas(TimeSheetListadoRequest request)
        {
            DateTime desde = DateTime.Parse(request.desde,System.Globalization.CultureInfo.InstalledUICulture);
            DateTime hasta = DateTime.Parse(request.hasta, System.Globalization.CultureInfo.InstalledUICulture);
            var usuarioID = _repoUsuario.Query.Where(x => x.UUID == request.UUID).Select(x => x.TimeSheetUsuarioID).FirstOrDefault();
            var usuarioIdParameter = new SqlParameter("@UsuarioId", usuarioID);
            var tipoHorarioIdParameter = new SqlParameter("@TipoHorarioId", request.tipoHorarioID);
            var desdeParameter = new SqlParameter("@Desde", desde.ToString("s") );
            var hastaIdParameter = new SqlParameter("@Hasta", hasta.ToString("s") );
            var TimeSheetLocationIdParameter = new SqlParameter("@TimeSheetLocationId", request.locationID);

            return _repoViewModel.StoreProcedure("REP_TimeSheet_Cons_sp @UsuarioId, @TipoHorarioId, @Desde, @Hasta, @TimeSheetLocationId"
                    , usuarioIdParameter, tipoHorarioIdParameter, desdeParameter, hastaIdParameter, TimeSheetLocationIdParameter).ToList().ParseTo<ReporteIngresoEgresoViewModel>();                    
        }        
    }
}