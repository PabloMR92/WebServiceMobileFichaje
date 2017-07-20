using System;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebServiceMobileFichaje.Repository
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _db;

        public BaseRepository(DbContext db)
        {
            this._db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Agregar(TEntity item)
        {
            Query.Add(item);
        }

        public IQueryable<TEntity> BuscarPor(Expression<Func<TEntity, bool>> predicado)
        {
            return Query.Where(predicado);
        }

        public void Eliminar(TEntity item)
        {
            Query.Remove(item);
        }

        public DbSet<TEntity> Query
        {
            get { return _db.Set<TEntity>(); }
        }

        public DbRawSqlQuery<TEntity> StoreProcedure(string nombre, params object[] parametros)
        {
            return _db.Database.SqlQuery<TEntity>(nombre, parametros);
        }
    }
}