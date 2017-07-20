using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;
using System.Data.Entity;
using WebServiceMobileFichaje.ViewModels;
using WebServiceMobileFichaje.Services;
using WebServiceMobileFichaje.Repository;
using WebServiceMobileFichaje.Context;
using WebServiceMobileFichaje.Validaciones;
using WebServiceMobileFichaje.Models;

namespace WebServiceMobileFichaje
{
    public static class UnityConfig
    {
        public static UnityContainer container = new UnityContainer();

        public static void RegisterComponents()
        {
            container.RegisterType<TimeSheetViewModelService, TimeSheetViewModelService>();
            container.RegisterType<DbContext, FichajeDBContext>();
            container.RegisterType<TimeSheetLocationService, TimeSheetLocationService>();            
            container.RegisterType<TimeSheetUsuarioService, TimeSheetUsuarioService>();
            container.RegisterType<TipoDeHorarioService, TipoDeHorarioService>();
            container.RegisterType<TimeSheetDomainService, TimeSheetDomainService>();            
            container.RegisterType<IValidacionUUID, ValidaCionUUIDMemoryDB>();
            container.RegisterType<IRepository<TipoDeHorario>, BaseRepository<TipoDeHorario>>();
            container.RegisterType<IRepository<TimeSheetTemporal>, BaseRepository<TimeSheetTemporal>>();
            container.RegisterType<IRepository<TimeSheetLocacion>, BaseRepository<TimeSheetLocacion>>();
            container.RegisterType<IRepository<TimeSheetDomain>, BaseRepository<TimeSheetDomain>>();
            container.RegisterType<IRepository<TimeSheetUsuario>, BaseRepository<TimeSheetUsuario>>();            
            container.RegisterType<IRepository<TimeSheetViewModel>, BaseRepository<TimeSheetViewModel>>();
            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}