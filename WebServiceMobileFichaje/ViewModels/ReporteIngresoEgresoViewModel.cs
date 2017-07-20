using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebServiceMobileFichaje.Models;

namespace WebServiceMobileFichaje.ViewModels
{
    public class ReporteIngresoEgresoViewModel 
    {
        public string Usuario { get; set; }
        public string TotalEntrada { get; set; }
        public string TotalSalida { get; set; }
        public string TotalTiempo { get; set; }
        public List<ReporteIngresoEgreso> Reportes { get; set; }
    }
}