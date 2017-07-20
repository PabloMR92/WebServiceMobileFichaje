using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Sql;
using System.Data.SqlClient;
using WebServiceMobileFichaje.Models;
using FluentValidation.Validators;

namespace WebServiceMobileFichaje.Validaciones
{
    public class ValidacionUsuarioDBLogin : PropertyValidator
    {

        public ValidacionUsuarioDBLogin() 
		: base("Usuario o contraseña incorrecta.") { }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var user = context.PropertyValue as LoginUsuario;
            var conn = System.Configuration.ConfigurationManager.ConnectionStrings["WebServiceFichajeConnectionString"].ConnectionString;
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(conn);
            builder.UserID = user.login;
            builder.Password = user.password;
            try
            {
                using (SqlConnection connection = new SqlConnection(builder.ToString()))
                {
                    connection.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}