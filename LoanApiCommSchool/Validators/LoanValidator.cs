using FluentValidation;
using LoanApiCommSchool.Models;

namespace LoanApiCommSchool.Validators
{
    public class LoanValidator : AbstractValidator<Loan>
    {
        public LoanValidator()
        {
            RuleFor(loan => loan.LoanType)
                .NotEmpty().WithMessage("LoanType is required.");

            RuleFor(loan => loan.Amount)
                .GreaterThan(0).WithMessage("Loan amount must be greater than 0.");

            RuleFor(loan => loan.Period)
                .GreaterThan(0).WithMessage("Loan period must be greater than 0.");

            RuleFor(loan => loan.Currency)
                .NotEmpty().WithMessage("Currency is required.")
                .MaximumLength(4).WithMessage("Currency code must be up to 4 characters.");
        }
    }
}
