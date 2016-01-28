using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using Moq;
using Uku.BusinessLogic.Service;
using Uku.Database.Model;
using Uku.Website.Config;
using Uku.Website.Controllers;
using Uku.Website.ViewModels;
using Xunit;

namespace Uku.Website.Tests.Controllers
{
    public class HomeControllersTests
    {
        [Fact]
        public void Index_Returns_All_Albums()
        {
            var albums = new List<Album>
            {
                new Album { AlbumId = 10, Title = "Ten" },
                new Album { AlbumId = 15, Title = "Fifteen" },
                new Album { AlbumId = 20, Title = "Twenty" }
            };

            var service = new Mock<IAlbumService>();
            service.Setup(x => x.GetAll()).Returns(albums);

            var mapper = AutoMapperConfig.Configure();

            var controller = new HomeController(mapper, service.Object);

            var result = controller.Index();

            Assert.IsType<ViewResult>(result);
            var viewResult = result as ViewResult;
            Assert.NotNull(viewResult.ViewData);
            Assert.NotNull(viewResult.ViewData.Model);
            Assert.IsType<HomeIndexViewModel>(viewResult.ViewData.Model);
            var viewModel = viewResult.ViewData.Model as HomeIndexViewModel;
            Assert.NotNull(viewModel.Albums);
            Assert.Equal(3, viewModel.Albums.Count);
            Assert.Equal(1, viewModel.Albums.Count(x => x.AlbumId == 10));
            Assert.Equal(1, viewModel.Albums.Count(x => x.AlbumId == 15));
            Assert.Equal(1, viewModel.Albums.Count(x => x.AlbumId == 20));
        }
    }
}
