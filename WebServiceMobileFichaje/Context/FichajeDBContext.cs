using System.Data.Entity;
using WebServiceMobileFichaje.Models;

namespace WebServiceMobileFichaje.Context
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
            modelBuilder.Entity<TimeSheetUsuario>().ToTable("TimeSheetUsuario");
            modelBuilder.Entity<TimeSheetTemporal>().ToTable("TimeSheetTemporal");
            modelBuilder.Entity<TipoDeHorario>().ToTable("TipoDeHorario");
            modelBuilder.Entity<TimeSheetDomain>().ToTable("TimeSheetDomain");
        }
    }
}