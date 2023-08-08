using System.Reflection;
using AspNetCoreAngular.Application.Behaviours;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreAngular.Application
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(
                typeof(IPipelineBehavior<,>),
                typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(
                typeof(IPipelineBehavior<,>),
                typeof(RequestValidationBehavior<,>));

            return services;
        }
    }
}
