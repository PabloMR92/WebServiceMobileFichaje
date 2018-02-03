using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebServiceMobileFichaje.Models;
using WebServiceMobileFichaje.Repository;
namespace WebServiceMobileFichaje.Services
{
    public class TimeSheetUsuarioService
    {
        private readonly IRepository<TimeSheetUsuario> _repo;
        private readonly DbContext _db;

        public TimeSheetUsuarioService(IRepository<TimeSheetUsuario> repo, DbContext db)
        {
            this._repo = repo;
            this._db = db;
        }

        public bool ExisteUUID(string uuid)
        {
            return _repo.Query.Any(x => x.UUID == uuid);
        }

        public bool ExisteUUIDParaUsuario(string login)
        {
            var usuarioID = new SqlParameter("@SQLLogin", login);
            var _timeSheetUsuarioID = _db.Database.SqlQuery<int>("TimeSheetUsuario_FromSqlLogin @SQLLogin", usuarioID).FirstOrDefault();
            var _usuario = _repo.Query.Where(x => x.TimeSheetUsuarioID == _timeSheetUsuarioID).FirstOrDefault();
            var UUID = _usuario.UUID;
            return (UUID != null || UUID == "");
        }

        public UUID AsignarUUID(LoginUsuario usuario)
        {
            var usuarioID = new SqlParameter("@SQLLogin", usuario.login);
            var _timeSheetUsuarioID = _db.Database.SqlQuery<int>("TimeSheetUsuario_FromSqlLogin @SQLLogin", usuarioID).FirstOrDefault();
            var _usuario = _repo.Query.Where(x => x.TimeSheetUsuarioID == _timeSheetUsuarioID).FirstOrDefault();
            _usuario.UUID = Guid.NewGuid().ToString();
            _repo.Save();
            return new UUID() { _UUID = _usuario.UUID };
        }
    }
}