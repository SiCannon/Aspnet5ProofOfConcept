using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;

namespace Scs.Entity.Base
{
    /// <summary>
    /// A layer between our application and Entity Framework which introduces some convenience methods to
    /// carry generic CRUD operations.
    /// </summary>
    public class BaseContext : DbContext //, IBaseContext? probably not required
    {
        public BaseContext(DbContextOptions options) : base(options)
        {

        }

        public TEntity Create<TEntity>(TEntity entity, bool commit = true) where TEntity : class
        {
            Set<TEntity>().Add(entity);

            if (commit)
            {
                SaveChanges();
            }

            return entity;
        }

        public TEntity Update<TEntity>(TEntity entity, bool commit = true) where TEntity : class
        {
            var set = Set<TEntity>();
            set.Attach(entity);
            Entry(entity).State = EntityState.Modified;

            if (commit)
            {
                SaveChanges();
            }

            return entity;
        }

        public TEntity Save<TEntity>(TEntity entity, bool isNew, bool commit = true) where TEntity : class
        {
            if (isNew)
            {
                return Create(entity, commit);
            }
            else
            {
                return Update(entity, commit);
            }
        }
    }
}
