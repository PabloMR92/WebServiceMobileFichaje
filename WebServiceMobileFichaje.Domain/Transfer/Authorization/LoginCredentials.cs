using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Threading;
using System.Globalization;

namespace WebServiceMobileFichaje.Domain.Transfer.Authorization
{
    public class LoginCredentials
    {
        [Required(ErrorMessage = "El campo usuario es requerido")]
        public string Username { get; set; }
        [Required(ErrorMessage = "El campo password es requerido")]
        public string Password { get; set; }
        [Required(ErrorMessage = "El campo dni es requerido")]
        public string Dni { get; set; }
    }
}