using ImageGallery.API.DAL.Services;
using ImageGallery.API.DAL.Settings;
using ImageGallery.API.Services.Pipelines;
using ImageGallery.API.Services.Requests;
using ImageGallery.API.Services.Responses;
using ImageGallery.API.Services.Services;
using ImageGallery.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ImageGallery.API
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
            services.AddControllers();

            services.AddDistributedRedisCache(config =>
            {
                config.Configuration = Configuration.GetConnectionString("redis");
            });

            services.AddTransient(typeof(IPipelineBehavior<GetImageRequest, GetImageResponse>), typeof(GetImagePipeline));
            services.AddTransientMediatrFor(typeof(GetAllImageIdsService)).WithProcessingPipeline();
            services.AddSingleton<ImageService>();

            services.Configure<ImageDatabaseSettings>(options => Configuration.GetSection("ImageDatabaseSettings").Bind(options));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
