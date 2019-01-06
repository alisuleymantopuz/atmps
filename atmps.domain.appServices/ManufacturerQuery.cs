using System;
using Microsoft.Extensions.Configuration;

namespace atmps.domain.appServices
{
    /// <summary>
    /// Manufacturer query.
    /// </summary>
    public class ManufacturerQuery : IQuery<string>
    {

    }

    /// <summary>
    /// Manufacturer query handler.
    /// </summary>
    public class ManufacturerQueryHandler : IQueryHandler<ManufacturerQuery, string>
    {
        public ManufacturerQueryHandler(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public string Handle(ManufacturerQuery query)
        {
            return Configuration[ConfigurationKeys.Manufacturer];
        }
    }
}
