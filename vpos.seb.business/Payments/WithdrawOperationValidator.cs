using FluentValidation;
using FluentValidation.Results;
using vpos.messages;

namespace vpos.seb.business.Payments
{
    /// <summary>
    /// Withdraw operation validator.
    /// </summary>
    public class WithdrawOperationValidator : AbstractValidator<WithdrawOperation>
    {
        public void SetupRules()
        {
            this.RuleFor(x => x.AccountNumber).NotEmpty().WithMessage(ResponseCodes.A1001.ToString());
            this.RuleFor(x => x.WithdrawAmount).NotEmpty().WithMessage(ResponseCodes.W1001.ToString());
            this.RuleFor(x => x.WithdrawAmount).GreaterThan(0).WithMessage(ResponseCodes.W1002.ToString());
        }

        public override ValidationResult Validate(ValidationContext<WithdrawOperation> context)
        {
            return context == null
                            ? new ValidationResult(new[] { new ValidationFailure("WithdrawOperation", ResponseCodes.W1001.ToString()) })
                            : base.Validate(context);
        }
    }
}
