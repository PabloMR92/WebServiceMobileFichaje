﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebServiceMobileFichaje.Models;
using WebServiceMobileFichaje.Repository;
using WebServiceMobileFichaje.ViewModels;

namespace WebServiceMobileFichaje.Services
{
    public class TimeSheetLocationService
    {
        private readonly IRepository<TimeSheetLocacion> _repo;

        public TimeSheetLocationService(IRepository<TimeSheetLocacion> repo)
        {
            this._repo = repo;
        }

        public List<TimeSheetLocationViewModel> GetLocations()
        {
            return _repo.Query.Select(x => new TimeSheetLocationViewModel()
            {
                CoordenadaX = x.CoordenadaX,
                CoordenadaY = x.CoordenadaY,
                TimeSheetLocationID = x.TimeSheetLocationID,
                RadioPermitido = x.RadioPermitido
            }).ToList();
        }

        public List<TimeSheetLocationListItemViewModel> GetLocationsItemList()
        {
            return _repo.Query.Select(x => new TimeSheetLocationListItemViewModel()
            {
                TimeSheetLocationID = x.TimeSheetLocationID,
                Descripcion = x.Descripcion
            }).ToList();
        }
    }
}