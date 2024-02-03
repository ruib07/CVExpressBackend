using CVExpress.Services.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace CVExpress.Services
{
    public static class DependencyRegister
    {
        public static void AddCustomServiceDependencyRegister(this IServiceCollection services, IConfiguration configuration)
        {
            services
            .Scan(scan => scan.FromAssemblies(typeof(RegisterUsersService).Assembly)
            .AddClasses(classe => classe.Where(p =>
                                               p.Name != null &&
                                               p.Name.EndsWith("Service") &&
                                               !p.IsInterface))
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

            services
            .Scan(scan => scan.FromAssemblies(typeof(UsersService).Assembly)
            .AddClasses(classe => classe.Where(p =>
                                               p.Name != null &&
                                               p.Name.EndsWith("Service") &&
                                               !p.IsInterface))
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

            services
            .Scan(scan => scan.FromAssemblies(typeof(HabilitationsService).Assembly)
            .AddClasses(classe => classe.Where(p =>
                                               p.Name != null &&
                                               p.Name.EndsWith("Service") &&
                                               !p.IsInterface))
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

            services
            .Scan(scan => scan.FromAssemblies(typeof(ExperienceService).Assembly)
            .AddClasses(classe => classe.Where(p =>
                                               p.Name != null &&
                                               p.Name.EndsWith("Service") &&
                                               !p.IsInterface))
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

            services
            .Scan(scan => scan.FromAssemblies(typeof(ContactsService).Assembly)
            .AddClasses(classe => classe.Where(p =>
                                               p.Name != null &&
                                               p.Name.EndsWith("Service") &&
                                               !p.IsInterface))
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        }
    }
}
