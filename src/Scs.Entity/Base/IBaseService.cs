namespace Scs.Entity.Base
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        TEntity Create(TEntity entity, bool commit = true);
        TEntity Update(TEntity entity, bool commit = true);
        void SaveChanges();
    }
}
