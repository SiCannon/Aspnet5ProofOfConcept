using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Uku.Database.Model;
using Uku.Website.ViewModels;

namespace Uku.Website.Config
{
    public static class AutoMapperConfig
    {
        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddSingleton(x => Configure());
        }

        public static IMapper Configure()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Album, HomeIndexAlbumViewModel>();
            });

            config.AssertConfigurationIsValid();

            return config.CreateMapper();
        }

    }
}
