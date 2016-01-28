using Microsoft.Data.Entity;

namespace Scs.Entity.BaseOld
{
    public class GenericRepo<TEntity, TContext> : BaseRepo<TContext>, IGenericRepo<TEntity>
        where TEntity : class
        where TContext : IBaseContext
    {
        public GenericRepo(TContext database) : base(database)
        {

        }

        public DbSet<TEntity> GetAll()
        {
            return database.GetDbSet<TEntity>();
        }

        public TEntity Create(TEntity entity, bool commit = true)
        {
            database.GetDbSet<TEntity>().Add(entity);

            if (commit)
            {
                database.SaveChanges();
            }

            return entity;
        }

        public TEntity Update(TEntity entity, bool commit = true)
        {
            var set = database.GetDbSet<TEntity>();
            set.Attach(entity);
            database.Modified(entity);

            if (commit)
            {
                database.SaveChanges();
            }

            return entity;
        }

    }
}
