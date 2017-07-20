using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServiceMobileFichaje.Models
{
    public class TimeSheet
    {
        public int TimeSheetID { get; set; }
        public DateTime Entrada  { get; set; }
        public DateTime Salida { get; set; }
        public int GrupoID { get; set; }
        public int ExternTimeSheetID  { get; set; }
        public int TimeSheetUsuarioID { get; set; }
        public int TipoHorarioID { get; set; }
        public string Observaciones { get; set; }
        public bool EntradaModificadaManualmente { get; set; }
        public bool SalidaModificadaManualmente { get; set; }
        public bool TimeSheetDesdeDispositivo { get; set; }
    }
}