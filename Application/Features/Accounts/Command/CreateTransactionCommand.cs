using Application.Repositories;
using Common.Enums;
using Common.Requests;
using Common.Wrapper;
using Domain;
using MediatR;

namespace Application.Features.Accounts.Command;

public class CreateTransactionCommand : IRequest<ResponseWrapper<int>>
{
    public Common.Requests.Transaction Transaction { get; set; }
}

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, ResponseWrapper<int>>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public CreateTransactionCommandHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ResponseWrapper<int>> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        // Find account
        var accountInDb = await _unitOfWork
            .ReadRepositoryFor<Account>()
            .GetByIdAsync(request.Transaction.AccountId);

        if (accountInDb == null)
        {
            return new ResponseWrapper<int>().Failed("Account not found");
        }

        // Know the type of transaction
        if (request.Transaction.Type == TransactionType.Withdrawal)
        {
            // Check if sufficient balance
            if (accountInDb.Balance < request.Transaction.Amount)
            {
                return new ResponseWrapper<int>().Failed("Insufficient funds");
            }

            var transaction = new Domain.Transaction()
            {
                AccountId = request.Transaction.AccountId,
                Amount = request.Transaction.Amount,
                Type = request.Transaction.Type,
                Date = DateTime.UtcNow
            };

            accountInDb.Balance -= request.Transaction.Amount;
            await _unitOfWork.WriteRepositoryFor<Domain.Transaction>().AddAsync(transaction);
            await _unitOfWork.WriteRepositoryFor<Account>().UpdateAsync(accountInDb);
            await _unitOfWork.CommitAsync(cancellationToken);

            return new ResponseWrapper<int>().Success(transaction.Id, "Withdrawal successful");
        }
        else if (request.Transaction.Type == TransactionType.Deposit)
        {
            var transaction = new Domain.Transaction()
            {
                AccountId = request.Transaction.AccountId,
                Amount = request.Transaction.Amount,
                Type = request.Transaction.Type,
                Date = DateTime.UtcNow
            };
            accountInDb.Balance += request.Transaction.Amount;
            await _unitOfWork.WriteRepositoryFor<Domain.Transaction>().AddAsync(transaction);
            await _unitOfWork.WriteRepositoryFor<Account>().UpdateAsync(accountInDb);
            await _unitOfWork.CommitAsync(cancellationToken);

            return new ResponseWrapper<int>().Success(transaction.Id, "Deposit successful");
        }

        return new ResponseWrapper<int>().Failed("Transaction type not found");
    }
}
