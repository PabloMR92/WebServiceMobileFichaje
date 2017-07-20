using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace WebServiceMobileFichaje.Models
{
    public class TimeSheetUsuario
    {
        [Required]
        [Display(Name = "TimeSheetUsuarioID")]
        [Key]
        public int TimeSheetUsuarioID { get; set; }
        [Required]
        [Display(Name = "Nombre")]
        [StringLength(50)]
        public string Nombre { get; set; }
        [Required]
        [Display(Name = "Apellido")]
        [StringLength(50)]
        public string Apellido { get; set; }
        public string LogIn { get; set; }
        public string IP { get; set; }
        public bool? Admin { get; set; }
        [Required]
        [Display(Name = "GrupoID")]
        public int GrupoID { get; set; }
        public int? ExternTimeSheetUsuarioID { get; set; }
        public string UUID { get; set; }
    }
}