using Microsoft.Extensions.Configuration;

namespace atmps.domain.appServices
{
    /// <summary>
    /// Serial number query.
    /// </summary>
    public class SerialNumberQuery:IQuery<string>
    {

    }

    /// <summary>
    /// Serial number query handler.
    /// </summary>
    public class SerialNumberQueryHandler : IQueryHandler<SerialNumberQuery, string>
    {
        public SerialNumberQueryHandler(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public string Handle(SerialNumberQuery query)
        {
            return Configuration[ConfigurationKeys.SerialNumber];
        }
    }
}
