using System;
using System.Net.Http;
using ImageGallery.DAL.Contexts;
using ImageGallery.Services.Interfaces;
using ImageGallery.Services.Pipelines;
using ImageGallery.Services.Requests;
using ImageGallery.Services.Responses;
using ImageGallery.Services.Services;
using ImageGallery.Services.Settings;
using ImageGallery.Shared.Extensions;
using ImageGallery.Shared.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Serilog;

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

            services.AddTransient(typeof(IPipelineBehavior<GalleryLoadRequest, GalleryLoadResponse>), typeof(GalleryLoadPipeline));
            services.AddTransientMediatrFor(typeof(GalleryLoadService)).WithProcessingPipeline();
            services.Configure<ImageApiSettings>(options => Configuration.GetSection("ImageApiSettings").Bind(options));

            services.AddHttpClient<IDatabasesSyncService, DatabasesSyncService>(client =>
            {
                client.BaseAddress = new Uri(Configuration.GetSection("ImageApiSettings")["BaseAddress"]);
            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
                var handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                return handler;
            });

            services.Scan(scan =>
            {
                scan.FromAssembliesOf(typeof(ISessionAccessor))
                .AddClasses(x => x.AssignableTo(typeof(ITransientService)))
                .AsImplementedInterfaces().WithTransientLifetime();
            });

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


            var syncService = serviceProvider.GetService<IDatabasesSyncService>();
            syncService.SyncImageGallery().GetAwaiter().GetResult();
            Console.WriteLine("Everything is set up. You can start using the app! :)");
        }
    }
}
