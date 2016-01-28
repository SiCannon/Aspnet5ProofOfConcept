using Microsoft.Data.Entity;

namespace Scs.Entity.BaseOld
{
    public class BaseContext : DbContext, IBaseContext
    {
        public DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }

        public void Added<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Added;
        }

        public void Modified<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Modified;
        }

        public void Unchanged<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Unchanged;
        }

        public void Deleted<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Deleted;
        }

    }
}
