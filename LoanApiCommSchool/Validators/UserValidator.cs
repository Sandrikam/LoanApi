using FluentValidation;
using LoanApiCommSchool.Models;

namespace LoanApiCommSchool.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.Username)
                .NotEmpty().WithMessage("Username is required.")
                .MinimumLength(5).WithMessage("Username must be at least 5 characters long.");

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");

            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(user => user.Age)
                .GreaterThanOrEqualTo(18).WithMessage("Age must be 18 or older.");

            RuleFor(user => user.MonthlyIncome)
                .GreaterThan(0).WithMessage("Monthly income must be greater than 0.");
        }
    }
}
