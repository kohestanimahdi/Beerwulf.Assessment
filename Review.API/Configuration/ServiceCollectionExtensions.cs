using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Review.Application.DomainServices;
using Review.Infrastructure.Persistance;
using Review.Infrastructure.Persistance.DataInitializer;
using Review.Infrastructure.Persistance.UnitOfWorks;
using System;
using System.IO;
using System.Reflection;

namespace Review.API.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection WithDbContext(this IServiceCollection services)
            => services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase("ReviewDataBase");
            });

        public static IServiceCollection WithSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Review API", Version = "v1" });

                var xmlFile = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetEntryAssembly().GetName().Name}.xml");
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });


            services.AddSwaggerGenNewtonsoftSupport();

            return services;
        }

        public static IServiceCollection WithUnitOfWorks(this IServiceCollection services)
        {
            services.AddScoped<IProductUnitOfWork, ProductUnitOfWork>();
            return services;
        }

        public static IServiceCollection WithDataInitializerServices(this IServiceCollection services)
        {
            services.AddScoped<IDataInitializer, ProductDataInitializer>();
            return services;
        }

        public static IServiceCollection WithDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            return services;
        }
    }
}
