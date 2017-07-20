using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;

namespace WebServiceMobileFichaje.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Save();
        void Agregar(TEntity item);
        void Eliminar(TEntity item);
        DbSet<TEntity> Query { get; }
        IQueryable<TEntity> BuscarPor(Expression<Func<TEntity, bool>> predicado);
        DbRawSqlQuery<TEntity> StoreProcedure(string nobre, params object[] parametros);
    }
}
