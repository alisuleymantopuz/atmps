using System.Collections.Generic;
using atmps.domain.appServices.Utilities;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace atmps.domain.appServices
{
    /// <summary>
    /// App services module.
    /// </summary>
    public static class AppServicesModule
    {
        public static void RegisterAppServicesModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IBalanceCommunication, BalanceCommunication>();
            services.AddTransient<IDepositCommunication, DepositCommunication>();
            services.AddTransient<IWithdrawCommunication, WithdrawCommunication>();
            services.AddTransient<ICardNumberHelper, CardNumberHelper>();
            services.AddTransient<IAuditLogProcessor, AuditLogProcessor>();
            services.AddTransient<ITransactionProcessor, TransactionProcessor>();
            services.AddTransient<ICardNumberValidationService, CardNumberValidationService>();
            services.AddTransient<ICardTypeChecker, CardTypeChecker>();
            services.AddTransient<ICardTypeChecker, CardTypeChecker>();
            services.AddTransient<IMoneyGenerator, MoneyGenerator>();

            services.AddTransient<IQuery<Result<decimal>>, BalanceQuery>();
            services.AddTransient<IQueryHandler<BalanceQuery, Result<decimal>>, BalanceQueryHandler>();

            services.AddTransient<IQuery<Result<Money>>, WithdrawQuery>();
            services.AddTransient<IQueryHandler<WithdrawQuery, Result<Money>>, WithdrawQueryHandler>();

            services.AddTransient<IQuery<string>, ManufacturerQuery>();
            services.AddTransient<IQueryHandler<ManufacturerQuery, string>, ManufacturerQueryHandler>();

            services.AddTransient<IQuery<string>, SerialNumberQuery>();
            services.AddTransient<IQueryHandler<SerialNumberQuery, string>, SerialNumberQueryHandler>();

            services.AddTransient<IQuery<Result<List<Fee>>>, FeeTransactionListQuery>();
            services.AddTransient<IQueryHandler<FeeTransactionListQuery, Result<List<Fee>>>, FeeTransactionListQueryHandler>();

            services.AddTransient<ICommand, DepositCommand>();
            services.AddTransient<ICommandHandler<DepositCommand>, DepositCommandHandler>();

            services.AddTransient<ICommand, InsertCardCommand>();
            services.AddTransient<ICommandHandler<InsertCardCommand>, InsertCardCommandHandler>();

            services.AddTransient<ICommand, ReturnCardCommand>();
            services.AddTransient<ICommandHandler<ReturnCardCommand>, ReturnCardCommandHandler>();

            services.AddSingleton<Messages>();
        }
    }
}
