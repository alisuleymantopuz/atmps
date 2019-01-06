using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace atmps.domain.infrastructure
{
    /// <summary>
    /// Domain infra module.
    /// </summary>
    public static class DomainInfraModule
    {
        public static void RegisterDomainInfraModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase(configuration["ConnectionString"]), ServiceLifetime.Singleton);
        }
    }
}
