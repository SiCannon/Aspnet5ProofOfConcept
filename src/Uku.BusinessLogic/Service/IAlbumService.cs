using System.Collections.Generic;
using Scs.Entity.Base;
using Uku.Database.Model;

namespace Uku.BusinessLogic.Service
{
    public interface IAlbumService : IBaseService<Album>
    {
        Album GetById(int id);
        List<Album> GetAll();
    }
}
