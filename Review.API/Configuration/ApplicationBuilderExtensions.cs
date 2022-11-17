using Microsoft.AspNetCore.Builder;
using Review.Infrastructure.Persistance.DataInitializer;
using Review.Infrastructure.Persistance;
using Microsoft.Extensions.DependencyInjection;

namespace Review.API.Configuration
{
    public static class ApplicationBuilderExtensions
    {
        public static void IntializeDatabase(this IApplicationBuilder app)
        {
            //Use C# 8 using variables
            using var scope = app.ApplicationServices.CreateScope();

            var dbcontext = scope.ServiceProvider.GetService<ApplicationDbContext>();
            dbcontext.Database.EnsureCreated();

            var dataInitializers = scope.ServiceProvider.GetServices<IDataInitializer>();
            foreach (var dataInitializer in dataInitializers)
                dataInitializer.InitializeData();
        }
    }
}
