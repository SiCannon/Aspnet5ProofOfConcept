using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity;
using Moq;
using Uku.BusinessLogic.Implement;
using Uku.Database.Model;
using Uku.Database.Persist;
using Xunit;

namespace Uku.BusinessLogic.Tests.Implement
{
    public class AlbumServiceTests
    {
        [Fact]
        public void GetAll_Gets_All()
        {
            var albums = new List<Album>
            {
                new Album { AlbumId = 10, Title = "Ten" },
                new Album { AlbumId = 15, Title = "Fifteen" },
                new Album { AlbumId = 20, Title = "Twenty" }
            }.AsQueryable();

            var dbset = new Mock<DbSet<Album>>();
            dbset.As<IQueryable<Album>>().Setup(x => x.Provider).Returns(albums.Provider);
            dbset.As<IQueryable<Album>>().Setup(x => x.Expression).Returns(albums.Expression);
            dbset.As<IQueryable<Album>>().Setup(x => x.ElementType).Returns(albums.ElementType);
            dbset.As<IQueryable<Album>>().Setup(x => x.GetEnumerator()).Returns(albums.GetEnumerator());

            var context = new Mock<IUkuContext>();
            context.Setup(x => x.Albums).Returns(dbset.Object);

            var service = new AlbumService(context.Object);
            var result = service.GetAll();

            Assert.Equal(3, result.Count);
        }
    }
}
