namespace Scs.Entity.Base
{
    public interface IBaseContext
    {
        TEntity Create<TEntity>(TEntity entity, bool commit = true) where TEntity : class;
        TEntity Update<TEntity>(TEntity entity, bool commit = true) where TEntity : class;
    }
}
