using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebServiceMobileFichaje.Models;
using WebServiceMobileFichaje.Repository;
using WebServiceMobileFichaje.Services;
using Microsoft.Practices.Unity;
using System.Data.Entity;

namespace WebServiceMobileFichaje.Validaciones
{
    public class ValidacionUsuarioUUIDSinAsignar : PropertyValidator
    {
        public ValidacionUsuarioUUIDSinAsignar()
        : base("Ya tiene un dispositivo asociado a la aplicación. Contáctese con un administrador.") {  }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var user = context.PropertyValue as LoginUsuario;
            var repo = UnityConfig.container.Resolve<IRepository<TimeSheetUsuario>>();
            var db = UnityConfig.container.Resolve<DbContext>();
            var service = new TimeSheetUsuarioService(repo, db);
            if (service.ExisteUUIDParaUsuario(user.login))
                return false;
            else
                return true; 
        }
    }
}