using Common.Enums;
using Common.Requests;
using FluentValidation;

namespace BankUI.Pages.Banking.Validators;

public class TransactionsValidator : AbstractValidator<TransactionRequest>
{
    public TransactionsValidator()
    {
        RuleFor(transaction => transaction.Type)
            .IsInEnum();

        RuleFor(transaction => transaction.Amount)
            .GreaterThan(0m)
            .When(transaction => transaction.Type == TransactionType.Deposit);

        RuleFor(transaction => transaction.Amount)
            .GreaterThan(0m)
            .LessThanOrEqualTo(transaction => transaction.CurrentBalance)
            .When(transaction => transaction.Type == TransactionType.Withdrawal)
            .WithMessage("Withdrawal amount must be less than or equal to current balance.");
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (requestModel, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<TransactionRequest>.CreateWithOptions((TransactionRequest)requestModel, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
        {
            return Array.Empty<string>();
        }
        return result.Errors.Select(error => error.ErrorMessage);
    };
}
