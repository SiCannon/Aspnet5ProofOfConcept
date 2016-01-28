using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNet.Mvc;
using Uku.BusinessLogic.Service;
using Uku.Website.ViewModels;

namespace Uku.Website.Controllers
{
    public class HomeController : Controller
    {
        IMapper mapper;
        IAlbumService albumService;

        public HomeController(IMapper mapper, IAlbumService albumService)
        {
            this.mapper = mapper;
            this.albumService = albumService;
        }

        public IActionResult Index()
        {
            var vm = new HomeIndexViewModel
            {
                Albums = mapper.Map<List<HomeIndexAlbumViewModel>>(albumService.GetAll())
            };

            return View(vm);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
