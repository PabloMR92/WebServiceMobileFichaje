using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServiceMobileFichaje.Models
{
    public class TimeSheetListadoRequest
    {
        public string UUID { get; set; }
        public int tipoHorarioID { get; set; }
        public string desde { get; set; }
        public string hasta { get; set; }
        public int locationID { get; set; }
    }
}