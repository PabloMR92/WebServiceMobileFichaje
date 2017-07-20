using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebServiceMobileFichaje.Models;
using WebServiceMobileFichaje.Repository;

namespace WebServiceMobileFichaje.Services
{
    public class TipoDeHorarioService
    {
        private readonly IRepository<TipoDeHorario> _repo;

        public TipoDeHorarioService(IRepository<TipoDeHorario> repo)
        {
            this._repo = repo;
        }
        
        public List<TipoDeHorario> GetTipoDeHorarioItemList()
        {
            return _repo.Query.ToList();
        }
    }
}