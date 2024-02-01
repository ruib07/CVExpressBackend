using CVExpress.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace CVExpress.API.Configurations.Persistance
{
    public static class PersistanceConfiguration
    {
        public static void AddCustomDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("CVExpressDb");

            services.AddDbContext<CVExpressDbContext>(options =>
            {
                options.UseSqlServer(connectionString, sqlServerOptions =>
                {
                    var assembly = typeof(CVExpressDbContext).Assembly;
                    var assemblyName = assembly.GetName();

                    sqlServerOptions.MigrationsAssembly(assemblyName.Name);
                    sqlServerOptions.EnableRetryOnFailure(
                        maxRetryCount: 2,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                });
            });
        }
    }
}
