using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebServiceMobileFichaje.Domain.EF.Model;
using WebServiceMobileFichaje.Domain.Repositories;
using WebServiceMobileFichaje.Domain.Transfer.Authorization;
using WebServiceMobileFichaje.Domain.Transfer.Business;

namespace WebServiceMobileFichaje.Domain.Services.Business
{
    public class ReportService
    {
        private readonly IRepository<EF.Model.InOutUserReport> _repo;

        public ReportService(IRepository<EF.Model.InOutUserReport> repo)
        {
            _repo = repo;
        }

        public async Task<InOutUserReportResult> Get(InOutUserReportRequest request)
        {
            var from = DateTime.Parse(request.From, System.Globalization.CultureInfo.InstalledUICulture);
            var to = DateTime.Parse(request.To, System.Globalization.CultureInfo.InstalledUICulture);
            var currentUserId = ApplicationUser.GetCurrent().UserId;

            var userIdParameter = new SqlParameter("@UsuarioId", currentUserId);
            var scheduleTypeIdParameter = new SqlParameter("@TipoHorarioId", request.ScheduleTypeId);
            var fromParameter = new SqlParameter("@Desde", from.ToString("s"));
            var toParameter = new SqlParameter("@Hasta", to.ToString("s"));
            var locacionIdParameter = new SqlParameter("@TimeSheetLocacionId", request.LocationId);

            var report = await _repo.ExecStoreProcedure<EF.Model.InOutUserReport>("REP_TimeSheet_Cons_sp @UsuarioId, @TipoHorarioId, @Desde, @Hasta, @TimeSheetLocacionId"
                    , userIdParameter, scheduleTypeIdParameter, fromParameter, toParameter, locacionIdParameter)
                    .ToListAsync();

            return Parse(report);
        }

        private InOutUserReportResult Parse(IEnumerable<EF.Model.InOutUserReport> report)
        {
            return report
                        .GroupBy(g => g.TimeSheetUsuarioID, r => r)
                        .Select(x => new InOutUserReportResult()
                        {
                            User = x.FirstOrDefault().Usuario,
                            InTotal = TimeSpan.FromSeconds(x.Average(y => y.SegundosEntrada)).ToString(),
                            OutTotal = TimeSpan.FromSeconds(x.Average(y => y.SegundosSalida)).ToString(),
                            Total = TimeSpan.FromSeconds(x.Sum(y => y.Total)).ToString(),
                            Report = x.Select(y => new Transfer.Business.InOutUserReport()
                            {
                                Description = y.Observaciones,
                                Date = y.Entrada.Date.ToString("dd/MM/yyyy"),
                                In = y.Entrada + " " + y.EntradaManual,
                                Out = y.Salida + " " + y.SalidaManual,
                                Total = y.Total
                            }).ToList()
                        })
                        .FirstOrDefault();
        }
    }
}