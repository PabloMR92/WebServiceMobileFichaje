using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebServiceMobileFichaje.Domain.EF.Model;
using WebServiceMobileFichaje.Domain.Exceptions;
using WebServiceMobileFichaje.Domain.Repositories;
using WebServiceMobileFichaje.Domain.Transfer.Authorization;
using WebServiceMobileFichaje.Domain.Transfer.Validation;
using WebServiceMobileFichaje.Domain.Utils;

namespace WebServiceMobileFichaje.Domain.Services.Authorization
{
    public enum ValidationType { Login = 1 }
    public enum UserLoginResultEnum
    {
        [Description("Ocurrio un error. Póngase en contacto con un administrador.")]
        Other = 0,
        Ok = 1,
        [Description("El DNI ingresado es incorrecto.")]
        DNIError = 2,
        [Description("El usuario ingresado es incorrecto.")]
        UsernameError = 3,
        [Description("La clave ingresada no es válida.")]
        PasswordError = 4
    }

    public class LoginService
    {
        public static string UserAlreadyHasDeviceAssociated = "El usuario ya tiene un dispositivo asociado. Comuníquese con un administrador.";
        private readonly IRepository<TimeSheetUsuario> _repo;

        public LoginService(IRepository<TimeSheetUsuario> repo)
        {
            _repo = repo;
        }

        private async Task<TimeSheetUsuario> GetUserFromCredentials(LoginCredentials credentials)
        {
            var dni = new SqlParameter("@Documento", credentials.Dni);
            var username = new SqlParameter("@Login", credentials.Username);
            var _timeSheetUsuarioID = await _repo.ExecStoreProcedure<int>("TimeSheetUsuario_FromLoginCredentials_sp @Login, @Documento", username, dni).FirstOrDefaultAsync();
            return await _repo.Where(x => x.TimeSheetUsuarioID == _timeSheetUsuarioID).FirstOrDefaultAsync();
        }

        public async Task<ValidationResult> ValidateCredentials(LoginCredentials credentials)
        {
            var loginResult = await Login(credentials);
            return ValidateLogin(loginResult);
        }

        public async Task<TimeSheetUsuario> GenerateUUID(LoginCredentials credentials)
        {
            var user = await GetUserFromCredentials(credentials);

            if(!string.IsNullOrEmpty(user.UUID))
               throw new BusinessValidationException(UserAlreadyHasDeviceAssociated);

            user.UUID = Guid.NewGuid().ToString();
            await _repo.SaveAsync();
            return user;
        }

        private async Task<UserLoginResult> Login(LoginCredentials credentials)
        {
            var dni = new SqlParameter("@Documento", credentials.Dni);
            var username = new SqlParameter("@Login", credentials.Username);
            var password = new SqlParameter("@Password", credentials.Password);
            var email = new SqlParameter("@Email", DBNull.Value);
            var validationType = new SqlParameter("@TipoValidacion", ValidationType.Login);
            return await _repo.ExecStoreProcedure<UserLoginResult>("Login_Cons_sp @Documento, @Login, @Password, @Email, @TipoValidacion", dni, username, password, email, validationType).FirstOrDefaultAsync();
        }

        private ValidationResult ValidateLogin(UserLoginResult result)
        {
            return Enum.GetValues(typeof(UserLoginResultEnum))
                .Cast<UserLoginResultEnum>()
                .Where((n, x) => x == result.Result)
                .DefaultIfEmpty(UserLoginResultEnum.Other)
                .Select(e => new ValidationResult()
                {
                    IsValid = e == UserLoginResultEnum.Ok,
                    ErrorMsg = e.GetEnumDescription()
                })
                .FirstOrDefault();
        }
    }
}