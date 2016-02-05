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

        public TEntity Create(TEntity entity, bool commit = true)
        {
            return context.Create(entity, commit);
        }

        public TEntity Update(TEntity entity, bool commit = true)
        {
            return context.Update(entity, commit);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
