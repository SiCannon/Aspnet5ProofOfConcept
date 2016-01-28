using Microsoft.Data.Entity;

namespace Scs.Entity.BaseOld
{
    public interface IBaseContext
    {
        int SaveChanges();
        DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class;
        void Added<TEntity>(TEntity entity) where TEntity : class;
        void Modified<TEntity>(TEntity entity) where TEntity : class;
        void Unchanged<TEntity>(TEntity entity) where TEntity : class;
        void Deleted<TEntity>(TEntity entity) where TEntity : class;
    }
}
