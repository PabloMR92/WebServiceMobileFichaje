using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServiceMobileFichaje.ViewModels
{
    public class TimeSheetLocationViewModel
    {
        public int TimeSheetLocationID { get; set; }
        public decimal? CoordenadaX { get; set; }
        public decimal? CoordenadaY { get; set; }
        public decimal? RadioPermitido { get; set; }
        public double Distancia { get; set; } 
    }
}