using Application.Repositories;
using Common.Responses;
using Common.Wrapper;
using Domain;
using Mapster;
using MediatR;

namespace Application.Features.Accounts.Queries;

public class GetAccountTransactionsQuery : IRequest<ResponseWrapper<List<TransactionResponse>>>
{
    public int AccountId { get; set; }
}

public class GetAccountTransactionsQueryHandler : IRequestHandler<GetAccountTransactionsQuery, ResponseWrapper<List<TransactionResponse>>>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetAccountTransactionsQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseWrapper<List<TransactionResponse>>> Handle(GetAccountTransactionsQuery request, CancellationToken cancellationToken)
    {
        var transactions = _unitOfWork.ReadRepositoryFor<Transaction>()
            .Entities
            .Where(t => t.AccountId == request.AccountId)
            .ToList();

        if (transactions.Count == 0)
        {
            return await Task.FromResult(new ResponseWrapper<List<TransactionResponse>>().Failed("No transactions found for the specified account."));
        }

        return await Task.FromResult(new ResponseWrapper<List<TransactionResponse>>().Success(transactions.Adapt<List<TransactionResponse>>(), "Transactions retrieved successfully"));
    }
}