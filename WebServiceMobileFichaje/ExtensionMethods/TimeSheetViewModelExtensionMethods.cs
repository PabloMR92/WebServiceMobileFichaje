using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebServiceMobileFichaje.ViewModels;

namespace WebServiceMobileFichaje.ExtensionMethods
{
    public static class TimeSheetViewModelExtensionMethods
    {
        public static List<ViewModels.ReporteIngresoEgresoViewModel> ParseTo<ReporteIngresoEgresoViewModel>(this List<TimeSheetViewModel> lista) where ReporteIngresoEgresoViewModel: new()
        {
            List<ViewModels.ReporteIngresoEgresoViewModel> modelo = null;
            if (lista.Count > 0)
            {
                modelo = lista
                            .GroupBy(g => g.TimeSheetUsuarioID, r => r)
                            .Select(x => new ViewModels.ReporteIngresoEgresoViewModel()
                            {
                                Usuario = x.FirstOrDefault().Usuario,
                                TotalEntrada = TimeSpan.FromSeconds(x.Average(y => y.SegundosEntrada)).ToString(),
                                TotalSalida = TimeSpan.FromSeconds(x.Average(y => y.SegundosSalida)).ToString(),
                                TotalTiempo = TimeSpan.FromSeconds(x.Sum(y => y.Total)).ToString(),
                                Reportes = x.Select(y => new Models.ReporteIngresoEgreso()
                                {
                                    Descripcion = y.Observaciones,
                                    Fecha = y.Entrada.Date.ToString("dd/MM/yyyy"),
                                    Entrada = y.Entrada + " " + y.EntradaManual,
                                    Salida = y.Salida + " " + y.SalidaManual,
                                    Total = y.Total
                                }).ToList()
                            })
                            .ToList();
            }
            else
            {
                modelo = new List<ViewModels.ReporteIngresoEgresoViewModel>() { new ViewModels.ReporteIngresoEgresoViewModel() };
            }
            return modelo;
        }
    }
}