using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServiceMobileFichaje.Models
{
    public class ReporteIngresoEgreso
    {
        public string Descripcion { get; set; }
        public string Fecha { get; set; }
        public string Entrada { get; set; }
        public string Salida { get; set; }
        public int Total { get; set; }
    }
}