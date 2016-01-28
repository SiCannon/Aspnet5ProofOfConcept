using Microsoft.Data.Entity;

namespace Scs.Entity.BaseOld
{
    public interface IGenericRepo<TEntity> where TEntity : class
    {
        DbSet<TEntity> GetAll();
        TEntity Create(TEntity entity, bool commit = true);
        TEntity Update(TEntity entity, bool commit = true);
    }
}
