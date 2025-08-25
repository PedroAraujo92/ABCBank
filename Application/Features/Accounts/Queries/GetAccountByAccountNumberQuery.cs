using Application.Repositories;
using Common.Responses;
using Common.Wrapper;
using Domain;
using Mapster;
using MediatR;

namespace Application.Features.Accounts.Queries;

public class GetAccountByAccountNumberQuery : IRequest<ResponseWrapper<AccountResponse>>
{
    public string AccountNumber { get; set; }
}

public class GetAccountByAccountNumberQueryHandler : IRequestHandler<GetAccountByAccountNumberQuery, ResponseWrapper<AccountResponse>>
{
    private readonly IUnitOfWork<int> _unitOfWork;
    public GetAccountByAccountNumberQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ResponseWrapper<AccountResponse>> Handle(GetAccountByAccountNumberQuery request, CancellationToken cancellationToken)
    {
        var accountInDb = _unitOfWork
            .ReadRepositoryFor<Account>()
            .Entities
            .Where(a => a.AccountNumber == request.AccountNumber)
            .FirstOrDefault();

        if (accountInDb == null)
        {
            return new ResponseWrapper<AccountResponse>().Failed("Account not found");
        }
        return new ResponseWrapper<AccountResponse>().Success(accountInDb.Adapt<AccountResponse>(), "Account retrieved successfully");
    }
}
