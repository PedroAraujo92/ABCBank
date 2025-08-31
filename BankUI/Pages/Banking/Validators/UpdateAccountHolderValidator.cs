using Common.Requests;
using FluentValidation;

namespace BankUI.Pages.Banking.Validators;

public class UpdateAccountHolderValidator : AbstractValidator<UpdateAccountHolder>
{
    public UpdateAccountHolderValidator()
    {
        RuleFor(accountHolder => accountHolder.FirstName)
            .NotEmpty().WithMessage("First Name is required.")
            .MaximumLength(50).WithMessage("First Name cannot exceed 50 characters.");
        RuleFor(accountHolder => accountHolder.LastName)
            .NotEmpty().WithMessage("Last Name is required.")
            .MaximumLength(50).WithMessage("Last Name cannot exceed 50 characters.");
        RuleFor(accountHolder => accountHolder.EmailAddress)
            .NotEmpty().WithMessage("Email Address is required.")
            .EmailAddress().WithMessage("A valid Email Address is required.")
            .MaximumLength(100).WithMessage("Email Address cannot exceed 100 characters.");
        RuleFor(accountHolder => accountHolder.ContactNumber)
            .NotEmpty().WithMessage("Contact Number is required.")
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("A valid Contact Number is required.")
            .MaximumLength(15).WithMessage("Contact Number cannot exceed 15 characters.");
    }
    public Func<object, string, Task<IEnumerable<string>>> ValidateValuesAsync => async (requestModel, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<UpdateAccountHolder>
            .CreateWithOptions((UpdateAccountHolder)requestModel, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors
                     .Where(e => e.PropertyName == propertyName)
                     .Select(e => e.ErrorMessage);
    };
}
