using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.WebHost;
using Ninject.Web.WebApi.FilterBindingSyntax;
using System;
using System.Data.Entity;
using System.Web;
using System.Web.Http.Filters;
using WebServiceMobileFichaje.Authorization;
using WebServiceMobileFichaje.Authorization.ActionFilter;
using WebServiceMobileFichaje.Authorization.Service;
using WebServiceMobileFichaje.Domain.EF.Context;
using WebServiceMobileFichaje.Domain.EF.Model;
using WebServiceMobileFichaje.Domain.Repositories;
using WebServiceMobileFichaje.Domain.Services.Authorization;
using WebServiceMobileFichaje.Domain.Services.Business;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(WebServiceMobileFichaje.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(WebServiceMobileFichaje.App_Start.NinjectWebCommon), "Stop")]


namespace WebServiceMobileFichaje.App_Start
{
    public static class NinjectWebCommon
    {

        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                Register(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void Register(IKernel kernel)
        {
            RegisterAuthorizationBindings(kernel);
            RegisterContextBindings(kernel);
            RegisterServicesBindings(kernel);
            RegisterRepositoriesBindings(kernel);
        }

        private static void RegisterAuthorizationBindings(IKernel kernel)
        {
            kernel.Bind<ITokenService>()
                .To<JWTService>();

            kernel.Bind<IAuthorizationService>()
                .To<AuthorizationService>();            

            kernel.BindHttpFilter<AuthorizeActionFilter>(FilterScope.Action)
                 .WhenActionMethodHas<JWTAuthorizeAttribute>();
        }

        private static void RegisterContextBindings(IKernel kernel)
        {
            kernel.Bind<DbContext>()
                .To<FichajeDBContext>();
        }

        private static void RegisterServicesBindings(IKernel kernel)
        {
            kernel.Bind<LoginService>()
                .To<LoginService>();

            kernel.Bind<LocationService>()
                .To<LocationService>();

            kernel.Bind<TimeSheetTemporalService>()
                .To<TimeSheetTemporalService>();

            kernel.Bind<ScheduleTypeService>()
                .To<ScheduleTypeService>();

            kernel.Bind<ReportService>()
               .To<ReportService>();
        }

        private static void RegisterRepositoriesBindings(IKernel kernel)
        {
            kernel.Bind<IRepository<TimeSheetUsuario>>()
                .To<BaseRepository<TimeSheetUsuario>>();

            kernel.Bind<IRepository<TimeSheetLocacion>>()
                .To<BaseRepository<TimeSheetLocacion>>();

            kernel.Bind<IRepository<TimeSheetTemporal>>()
                .To<BaseRepository<TimeSheetTemporal>>();

            kernel.Bind<IRepository<TipoDeHorario>>()
                .To<BaseRepository<TipoDeHorario>>();

            kernel.Bind<IRepository<InOutUserReport>>()
                .To<BaseRepository<InOutUserReport>>();            
        }
    }
}
