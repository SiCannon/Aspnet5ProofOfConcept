using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Scs.Entity.Base;
using Uku.Database.Model;

namespace Uku.Database.Persist
{
    public class UkuContext : BaseContext, IUkuContext
    {
        // Need to implement this constructor and pass the options down, otherwise the DI container will not initialize
        public UkuContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Album> Albums { get; set; }
    }
}
