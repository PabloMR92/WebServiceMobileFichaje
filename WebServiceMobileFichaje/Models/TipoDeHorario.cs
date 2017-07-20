using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebServiceMobileFichaje.Models
{
    public class TipoDeHorario
    {
        [Key]
        public int TipoHorarioID { get; set; }
        public string Descripcion { get; set; }  
    }
}