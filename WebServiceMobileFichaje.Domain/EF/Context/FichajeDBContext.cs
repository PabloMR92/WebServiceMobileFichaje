using EntityFramework.DynamicFilters;
using System.Data.Entity;
using WebServiceMobileFichaje.Domain.EF.Model;
using WebServiceMobileFichaje.Domain.Transfer.Authorization;

namespace WebServiceMobileFichaje.Domain.EF.Context
{
    public class FichajeDBContext : DbContext
    {
        public FichajeDBContext() : base("name=WebServiceFichajeConnectionString") { }
        public DbSet<TimeSheet> TimeSheets { get; set; }
        public DbSet<TimeSheetUsuario> TimeSheetUsuario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<FichajeDBContext>(null);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TimeSheetLocacion>().ToTable("TimeSheetLocacion");
            modelBuilder.Entity<TimeSheetDispositivo>().ToTable("TimeSheetDispositivo");
            modelBuilder.Entity<TimeSheetUsuario>().ToTable("TimeSheetUsuario");
            modelBuilder.Entity<TimeSheetTemporal>().ToTable("TimeSheetTemporal");
            modelBuilder.Entity<TipoDeHorario>().ToTable("TipoDeHorario");

            modelBuilder.Filter("MultiTenant", (IMultiTenant d) => d.GrupoID, () => ApplicationUser.GetCurrent().GrupoId);
        }
    }
}