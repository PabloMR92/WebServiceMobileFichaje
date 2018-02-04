using FluentValidation.Validators;
using Microsoft.Practices.Unity;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using WebServiceMobileFichaje.Models;

namespace WebServiceMobileFichaje.Validaciones
{
    public class ValidacionUsuarioDBLogin : PropertyValidator
    {
        private readonly DbContext db;

        public ValidacionUsuarioDBLogin()
        : base("Usuario o contraseña incorrecta.")
        {
            this.db = UnityConfig.container.Resolve<DbContext>();
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var user = context.PropertyValue as LoginUsuario;
            var loginResult = Login(user);
            if (loginResult.Result == 1)
                return true;
            else if (loginResult.Result == 2)
            {
                context.Rule.MessageBuilder = _ => "El DNI ingresado es Incorrecto";
                return false;
            }
            else if (loginResult.Result == 3)
                return false;
            else if (loginResult.ClaveCaducada)
            {
                context.Rule.MessageBuilder = _ => "Su clave cauduco. Póngase en contacto con un administrador.";
                return false;
            }
            return false;
        }

        private LoginUsuarioResult Login(LoginUsuario credenciales)
        {
            var documentoUsuario = new SqlParameter("@Documento", credenciales.dni);
            var loginUsuario = new SqlParameter("@Login", credenciales.login);
            var passwordUsuario = new SqlParameter("@Password", credenciales.password);
            return db.Database.SqlQuery<LoginUsuarioResult>("Login_Cons_sp @Documento, @Login, @Password", documentoUsuario, loginUsuario, passwordUsuario).FirstOrDefault();
        }
    }
}