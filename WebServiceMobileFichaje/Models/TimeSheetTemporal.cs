using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebServiceMobileFichaje.Models
{
    public class TimeSheetTemporal
    {
        [Key]
        public int TimeSheetTemporalID { get; set; }
        public string LogIn { get; set; }
        public string Fecha  { get; set; }
        public int TimeSheetLocationID { get; set; }
        public int NumeroDispositivo { get; set; }
        public int GrupoID { get; set; }
    }
}