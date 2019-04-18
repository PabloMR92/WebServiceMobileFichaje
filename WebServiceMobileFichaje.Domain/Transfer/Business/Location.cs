using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServiceMobileFichaje.Domain.Transfer.Business
{
    public class Location
    {
        public int TimeSheetLocationID { get; set; }
        public decimal? CoordenadaX { get; set; }
        public decimal? CoordenadaY { get; set; }
        public decimal? RadioPermitido { get; set; }
        public double Distancia { get; set; }
        public string Descripcion { get; set; }
    }
}