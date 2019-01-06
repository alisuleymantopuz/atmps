using FluentValidation;
using FluentValidation.Results;
using vpos.messages;

namespace vpos.seb.business.Payments
{
    /// <summary>
    /// Balance operation validator.
    /// </summary>
    public class BalanceOperationValidator : AbstractValidator<BalanceOperation>
    {
        public void SetupRules()
        {
            this.RuleFor(x => x.AccountNumber).NotEmpty().WithMessage(ResponseCodes.A1003.ToString());
        }

        public override ValidationResult Validate(ValidationContext<BalanceOperation> context)
        {
            return context == null
                            ? new ValidationResult(new[] { new ValidationFailure("BalanceOperation", ResponseCodes.A1003.ToString()) })
                            : base.Validate(context);
        }
    }
}
