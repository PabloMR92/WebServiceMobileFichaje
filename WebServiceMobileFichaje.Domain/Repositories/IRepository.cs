using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebServiceMobileFichaje.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task SaveAsync();
        void Add(TEntity item);
        void Remove(TEntity item);
        DbSet<TEntity> Query { get; }
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        DbRawSqlQuery<TResult> ExecStoreProcedure<TResult>(string name, params object[] parameters);
    }
}