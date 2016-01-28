using System.Collections.Generic;
using Uku.Database.Model;

namespace Uku.BusinessLogic.Service
{
    public interface IAlbumService
    {
        Album GetById(int id);
        List<Album> GetAll();
    }
}
