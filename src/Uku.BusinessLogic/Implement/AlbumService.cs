using System;
using System.Collections.Generic;
using System.Linq;
using Scs.Entity.Base;
using Uku.BusinessLogic.Service;
using Uku.Database.Model;
using Uku.Database.Persist;

namespace Uku.BusinessLogic.Implement
{
    public class AlbumService : BaseService<Album, IUkuContext>, IAlbumService
    {
        public AlbumService(IUkuContext context) : base(context)
        {
            
        }

        public List<Album> GetAll()
        {
            return context.Albums.OrderBy(x => x.Title).ToList();
        }


        public Album GetById(int id)
        {
            return context.Albums.SingleOrDefault(x => x.AlbumId == id);
        }
    }
}
