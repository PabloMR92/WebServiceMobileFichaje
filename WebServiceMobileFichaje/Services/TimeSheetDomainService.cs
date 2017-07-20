using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebServiceMobileFichaje.Models;
using WebServiceMobileFichaje.Repository;

namespace WebServiceMobileFichaje.Services
{
    public class TimeSheetDomainService
    {
        private readonly IRepository<TimeSheetDomain> _repo;

        public TimeSheetDomainService(IRepository<TimeSheetDomain> repo)
        {
            this._repo = repo;
        }

        public List<TimeSheetDomain> GetDomainsItemList()
        {
            return _repo.Query.ToList();
        }
    }
}