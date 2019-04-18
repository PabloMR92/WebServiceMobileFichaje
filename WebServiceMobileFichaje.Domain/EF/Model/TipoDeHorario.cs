using System.ComponentModel.DataAnnotations;

namespace WebServiceMobileFichaje.Domain.EF.Model
{
    public class TipoDeHorario
    {
        [Key]
        public int TipoHorarioID { get; set; }
        public string Descripcion { get; set; }
    }
}