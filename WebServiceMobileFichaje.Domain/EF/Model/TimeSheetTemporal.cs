using System.ComponentModel.DataAnnotations;

namespace WebServiceMobileFichaje.Domain.EF.Model
{
    public class TimeSheetTemporal : IMultiTenant
    {
        [Key]
        public int TimeSheetTemporalID { get; set; }
        public string LogIn { get; set; }
        public string Fecha { get; set; }
        public int TimeSheetLocacionID { get; set; }
        public int NumeroDispositivo { get; set; }
        public int GrupoID { get; set; }
        public decimal? CoordenadaX { get; set; }
        public decimal? CoordenadaY { get; set; }
        public double Distancia { get; set; }
    }
}