using atmps.domain.appServices.Exceptions;
using CSharpFunctionalExtensions;

namespace atmps.domain.appServices
{
    /// <summary>
    /// Insert card command.
    /// </summary>
    public class InsertCardCommand : ICommand
    {
        public string CardNumber { get; set; }

        public InsertCardCommand(string cardNumber)
        {
            CardNumber = cardNumber;
        }
    }

    /// <summary>
    /// Insert card command handler.
    /// </summary>
    public sealed class InsertCardCommandHandler : ICommandHandler<InsertCardCommand>
    {
        public InsertCardCommandHandler(IAuditLogProcessor auditLogProcessor, 
        ICardNumberValidationService cardNumberValidationService)
        {
            AuditLogProcessor = auditLogProcessor;
            CardNumberValidationService = cardNumberValidationService;
        }

        public IAuditLogProcessor AuditLogProcessor { get; }
        public ICardNumberValidationService CardNumberValidationService { get; }

        public Result Handle(InsertCardCommand command)
        {
            var cardInfo = CardNumberValidationService.PopulateCardInfo(command.CardNumber);

            if (string.IsNullOrEmpty(cardInfo.AccountNumber))
            {
                AuditLogProcessor.Process(command.CardNumber, OperationType.InsertCard, false);
                return Result.Fail("Account number is not found!");
            }

            AuditLogProcessor.Process(command.CardNumber, OperationType.InsertCard, true);

            return Result.Ok();
        }
    }
}
