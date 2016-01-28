using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Uku.BusinessLogic.Implement;
using Uku.BusinessLogic.Service;
using Uku.Database.Model;
using Uku.Database.Persist;
using Uku.Website.Config;

namespace Uku.Website
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var path = PlatformServices.Default.Application.ApplicationBasePath;

            services.AddEntityFramework()
                .AddSqlite()
                .AddDbContext<UkuContext>(options =>
                    options.UseSqlite("Filename=" + Path.Combine(path, "uku.db")));

            // Add framework services.
            services.AddMvc();

            // Need to do this if the UkuContext does not inlcude a constructor which takes options. That's only a problem for the interface though, not the concrete type. No idea why.
            //services.AddTransient<IUkuContext, UkuContext>(p => p.GetRequiredService<UkuContext>());

            services
                .AddScoped<IUkuContext, UkuContext>()
                .AddScoped<IAlbumService, AlbumService>();

            services.AddAutoMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseIISPlatformHandler();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            CreateTestData(app, env);
        }

        private void CreateTestData(IApplicationBuilder app, IHostingEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            using (var db = serviceScope.ServiceProvider.GetService<UkuContext>())
            {
                db.Database.Migrate();

                if (db.Albums.Count() == 0)
                {
                    db.Albums.Add(new Album { Title = "Pet Sounds" });
                    db.Albums.Add(new Album { Title = "Abbey Road" });
                    db.Albums.Add(new Album { Title = "Meddle" });
                    db.Albums.Add(new Album { Title = "Nevermind" });
                    db.SaveChanges();
                }
            }
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
