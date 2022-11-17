using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Review.Infrastructure.Persistance;
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
    }
}
