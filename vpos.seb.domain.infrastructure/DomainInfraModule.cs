using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace vpos.seb.domain.infrastructure
{
    public static class DomainInfraModule
    {
        public static void RegisterDomainInfraModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase(configuration["ConnectionString"]), ServiceLifetime.Singleton);
        }
    }
}
