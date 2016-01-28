using Microsoft.Data.Entity;
using Scs.Entity.Base;
using Uku.Database.Model;

namespace Uku.Database.Persist
{
    public interface IUkuContext : IBaseContext
    {
        DbSet<Album> Albums { get; set; }
    }
}
