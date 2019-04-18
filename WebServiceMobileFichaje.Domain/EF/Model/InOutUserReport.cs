using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServiceMobileFichaje.Domain.EF.Model
{
    public class InOutUserReport
    {
        public int TimeSheetUsuarioID { get; set; }
        public string Usuario { get; set; }
        public string Descripcion { get; set; }
        public DateTime Entrada { get; set; }
        public string EntradaManual { get; set; }
        public DateTime Salida { get; set; }
        public string SalidaManual { get; set; }
        public int Total { get; set; }
        public int SegundosEntrada { get; set; }
        public int SegundosSalida { get; set; }
        public string Observaciones { get; set; }
    }
}