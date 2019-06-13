using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebServiceMobileFichaje.Domain.EF.Model
{
    public class TimeSheetLocacion : IMultiTenant
    {
        [Key]
        public int TimeSheetLocacionID { get; set; }
        public string Descripcion { get; set; }
        public int GrupoID { get; set; }
        public decimal? CoordenadaX { get; set; }
        public decimal? CoordenadaY { get; set; }
        public decimal? RadioPermitido { get; set; }
        public virtual ICollection<TimeSheetDispositivo> Dispositivos { get; set; }
    }
}