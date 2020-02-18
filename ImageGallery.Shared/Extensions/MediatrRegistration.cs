using ImageGallery.Shared.Pipelines;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace ImageGallery.Shared.Extensions
{
    public static class MediatrRegistration
    {
        public static ScrutorDecorator AddTransientMediatrFor(this IServiceCollection services, Type @typeof)
        {
            services.AddMediatR((config) =>
            {
                config.AsTransient();
            },
                Assembly.GetAssembly(@typeof)
            );

            return new ScrutorDecorator(services);
        }
    }

    public class ScrutorDecorator
    {
        private readonly IServiceCollection _services;
        public ScrutorDecorator(IServiceCollection services)
        {
            _services = services;
        }

        public void WithProcessingPipeline()
        {
            _services.Decorate(typeof(IPipelineBehavior<,>), typeof(ProcessingPipeline<,>));
        }
    }
}


