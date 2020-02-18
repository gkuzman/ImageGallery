using System;
using ImageGallery.DAL.Contexts;
using ImageGallery.Services.Services;
using ImageGallery.Shared.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace ImageGallery
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddDistributedSqlServerCache(x =>
            {
                x.ConnectionString = string.Format(Configuration.GetConnectionString("SqlCache"), Environment.GetEnvironmentVariable("SA_PASSWORD"));
                x.SchemaName = "dbo";
                x.TableName = "CacheTable";
            });

            services.AddDbContextPool<ImageGalleryContext>(options =>
            {
                options.UseSqlServer(string.Format(Configuration.GetConnectionString("ImageGalleryContext"), Environment.GetEnvironmentVariable("SA_PASSWORD")));
            });

            services.AddTransientMediatrFor(typeof(GalleryLoadService)).WithProcessingPipeline();

            services.AddSession(options =>
            {
                options.Cookie = new CookieBuilder { IsEssential = true, Name = "imagegallery.session" };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            using (var dbContext = serviceProvider.GetService<ImageGalleryContext>())
            {
                dbContext.Database.Migrate();
            };

            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
