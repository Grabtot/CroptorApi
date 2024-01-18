using Croptor.Application.Common.Interfaces.Persistence;
using Croptor.Infrastructure.Persistence;
using Croptor.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Croptor.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabase(configuration);
            services.AddRepositories();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("Postgres")
                ?? throw new Exception("No Postgres connection string provided");

            services.AddDbContext<CroptorDbContext>(options =>
            {
                options.UseNpgsql(connectionString);

            });

            //if (configuration["ASPNETCORE_ENVIRONMENT"] != "Development")
            //{
            //    using IServiceScope scope = services.BuildServiceProvider().CreateScope();

            //    CroptorDbContext context = scope.ServiceProvider.
            //        GetRequiredService<CroptorDbContext>();
            //    context.Database.Migrate();
            //}

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPresetRepository, PresetRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            return services;
        }
    }
}
