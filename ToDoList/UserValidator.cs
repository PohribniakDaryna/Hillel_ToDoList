using FluentValidation;
using FluentValidation.Results;
using ToDoList.Models;

namespace ToDoList
{
    public class UserValidator : AbstractValidator<LoginModel>, IUserValidator
    {
        public UserValidator()
        {
            RuleFor(u => u.Login).MinimumLength(6).MaximumLength(12).NotNull();
            RuleFor(p => p.Password).NotEmpty().WithMessage("Your password cannot be empty")
                    .MinimumLength(8).WithMessage("Your password length must be at least 8.")
                    .MaximumLength(16).WithMessage("Your password length must not exceed 16.")
                    .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                    .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                    .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
                    .Matches(@"[\!\?\*\.]+").WithMessage("Your password must contain at least one (!? *.).");
        }

        public bool ValidateLoginModel(LoginModel login)
        {
            UserValidator valid = new ();
            ValidationResult results = valid.Validate(login);

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }
                return false;
            }
            return true;
        }
    }
}