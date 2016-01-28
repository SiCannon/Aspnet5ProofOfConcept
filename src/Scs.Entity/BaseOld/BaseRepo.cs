namespace Scs.Entity.BaseOld
{
    public class BaseRepo<TContext> where TContext : IBaseContext
    {
        protected TContext database;

        public BaseRepo(TContext database)
        {
            this.database = database;
        }
    }
}
