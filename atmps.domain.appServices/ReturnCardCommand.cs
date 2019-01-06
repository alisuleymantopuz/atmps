using CSharpFunctionalExtensions;

namespace atmps.domain.appServices
{
    /// <summary>
    /// Return card command.
    /// </summary>
    public class ReturnCardCommand:ICommand
    {
        public string CardNumber { get; set; }
        public OperationType OperationType { get; set; }

        public ReturnCardCommand(string cardNumber)
        {
            CardNumber = cardNumber;
        }
    }

    /// <summary>
    /// Return card command handler.
    /// </summary>
    public sealed class ReturnCardCommandHandler : ICommandHandler<ReturnCardCommand>
    {
        public ReturnCardCommandHandler(IAuditLogProcessor auditLogProcessor)
        {
            AuditLogProcessor = auditLogProcessor;
        }

        public IAuditLogProcessor AuditLogProcessor { get; }

        public Result Handle(ReturnCardCommand command)
        {
            AuditLogProcessor.Process(command.CardNumber, OperationType.ReturnCard, true);

            return Result.Ok();
        }
    }
}
