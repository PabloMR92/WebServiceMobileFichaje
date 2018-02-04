using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using WebServiceMobileFichaje.Models;

namespace WebServiceMobileFichaje.Validaciones
{
    public class LoginValidator : AbstractValidator<LoginUsuario>
    {
        public LoginValidator()
        {
            RuleFor(model => model.dni).NotEmpty().WithMessage("El DNI es requerido");
            RuleFor(model => model.login).NotEmpty().WithMessage("El Usuario es requerido"); 
            RuleFor(model => model.password).NotEmpty().WithMessage("La contraseña es requerida"); 
            When(x => (x.login != null && x.login != "") && (x.password != null && x.password != ""), () => {
                RuleFor(model => model)         
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithName("login")
                .SetValidator(new ValidacionUsuarioDBLogin())
                .SetValidator(new ValidacionUsuarioUUIDSinAsignar());
            });
           
        }
        
    }
}