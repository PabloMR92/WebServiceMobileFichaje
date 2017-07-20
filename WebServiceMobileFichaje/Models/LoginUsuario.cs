using FluentValidation.Attributes;
using System.ComponentModel.DataAnnotations;
using WebServiceMobileFichaje.Validaciones;

namespace WebServiceMobileFichaje.Models
{
    [Validator(typeof(LoginValidator))]
    public class LoginUsuario
    {
        public string login { get; set; }
        public string password { get; set; }
    }
}