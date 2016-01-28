namespace Scs.Entity.Base
{
    public class BaseService<TEntity, TContext>
        where TEntity : class
        where TContext : IBaseContext
    {
        protected TContext context;

        public BaseService(TContext context)
        {
            this.context = context;
        }

        public TEntity Create(TEntity entity)
        {
            return context.Create(entity);
        }

        public TEntity Update(TEntity entity)
        {
            return context.Update(entity);
        }
    }
}
