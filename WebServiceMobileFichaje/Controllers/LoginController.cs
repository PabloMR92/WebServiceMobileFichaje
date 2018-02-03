using Microsoft.Practices.Unity;
using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;
using WebServiceMobileFichaje.Models;
using WebServiceMobileFichaje.Repository;
using WebServiceMobileFichaje.Services;

namespace WebServiceMobileFichaje.Controllers
{
    public class LoginController : ApiController
    {
        private readonly TimeSheetUsuarioService _service;
        private readonly DbContext _db;

        public LoginController(TimeSheetUsuarioService service, DbContext db)
        {
            this._db = db;
            _service = service;
        }
       
        [HttpPost]
        public IHttpActionResult Post(LoginUsuario usuario)
        {
            if (usuario == null)
               return BadRequest();
            if (ModelState.IsValid)
               return Ok(_service.AsignarUUID(usuario));
            else
               return BadRequest(ModelState);
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromUri] string login)
        {
            var userRepo = UnityConfig.container.Resolve<IRepository<TimeSheetUsuario>>();
            var usuarioID = new SqlParameter("@SQLLogin", login);
            var _timeSheetUsuarioID = _db.Database.SqlQuery<int>("TimeSheetUsuario_FromSqlLogin @SQLLogin", usuarioID).FirstOrDefault();
            var _usuario = userRepo.Query.Where(x => x.TimeSheetUsuarioID == _timeSheetUsuarioID).FirstOrDefault();

            _usuario.UUID = null;

            userRepo.Save();
            return Ok();
        }
    }
}
