using System.Collections.Generic;
using System.Linq;
using atmps.domain.infrastructure;
using CSharpFunctionalExtensions;

namespace atmps.domain.appServices
{
    public class FeeTransactionListQuery : IQuery<Result<List<Fee>>>
    {

    }

    public class FeeTransactionListQueryHandler : IQueryHandler<FeeTransactionListQuery, Result<List<Fee>>>
    {
        public FeeTransactionListQueryHandler(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }

        public AppDbContext AppDbContext { get; }

        public Result<List<Fee>> Handle(FeeTransactionListQuery query)
        {
            var result = AppDbContext.Transactions
                        .Where(x => x.TransactionType == TransactionType.WITHDRAW && x.OperationResult == true)
                        .Select(x => new Fee
                        {
                            CardNumber = x.CardNumber,
                            WithdrawalDate = x.TransactionDate,
                            WithdrawalFeeAmount = x.TransactionFee ?? 0,
                            WithdrawalTotalAmount = x.WithdrawAmount ?? 0
                        })
                        .ToList();

            return Result.Ok(result);
        }
    }
}
