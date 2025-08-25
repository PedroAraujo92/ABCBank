using Application.Repositories;
using Common.Responses;
using Common.Wrapper;
using Mapster;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace Application.Features.Accounts.Queries;

public class GetAccountsQuery : IRequest<ResponseWrapper<List<AccountResponse>>>
{

}

public class GetAccountsQueryHandler : IRequestHandler<GetAccountsQuery, ResponseWrapper<List<AccountResponse>>>
{
    private readonly IUnitOfWork<int> _unitOfWork;
    public GetAccountsQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ResponseWrapper<List<AccountResponse>>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
    {
        var accountsInDb = await _unitOfWork
            .ReadRepositoryFor<Domain.Account>()
            .GetAllAsync();

        if (accountsInDb == null || accountsInDb.Count == 0)
        {
            return new ResponseWrapper<List<AccountResponse>>().Failed("No accounts found");
        }

        return new ResponseWrapper<List<AccountResponse>>().Success(accountsInDb.Adapt<List<AccountResponse>>(), "Accounts retrieved successfully");
    }
}