using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebServiceMobileFichaje.Domain.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _db;

        public BaseRepository(DbContext db)
        {
            this._db = db;
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public void Add(TEntity item)
        {
            Query.Add(item);
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicado)
        {
            return Query.Where(predicado);
        }

        public void Remove(TEntity item)
        {
            Query.Remove(item);
        }

        public DbSet<TEntity> Query
        {
            get { return _db.Set<TEntity>(); }
        }

        public DbRawSqlQuery<TResult> ExecStoreProcedure<TResult>(string name, params object[] parameters)
        {
            return _db.Database.SqlQuery<TResult>(name, parameters);
        }
    }
}