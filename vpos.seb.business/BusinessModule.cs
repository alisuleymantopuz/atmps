using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using vpos.seb.business.Payments;

namespace vpos.seb.business
{
    /// <summary>
    /// Business module for container registration
    /// </summary>
    public static class BusinessModule
    {
        public static void RegisterBusinessModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IPaymentManager, PaymentManager>();
            services.AddTransient<BalanceOperationValidator>();
            services.AddTransient<DepositOperationValidator>();
            services.AddTransient<WithdrawOperationValidator>();
        }
    }
}
