using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebServiceMobileFichaje.Models
{
    public class TimeSheetLocacion
    {
        [Key]
        public int TimeSheetLocacionID { get; set; }
        public string Descripcion { get; set; }
        public int GrupoID { get; set; }
        public decimal CoordenadaX { get; set; }
        public decimal CoordenadaY { get; set; }
        public decimal RadioPermitido { get; set; }
    }
}