using Application.Repositories;
using Common.Responses;
using Common.Wrapper;
using Domain;
using Mapster;
using MediatR;

namespace Application.Features.Accounts.Queries;

public class GetAccountsByAccountHolderId : IRequest<ResponseWrapper<List<AccountResponse>>>
{
    public int AccountHolderId { get; set; }

}

public class GetAccountsByAccountHolderIdHandler : IRequestHandler<GetAccountsByAccountHolderId, ResponseWrapper<List<AccountResponse>>>
{
    private readonly IUnitOfWork<int> _unitOfWork;
    public GetAccountsByAccountHolderIdHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ResponseWrapper<List<AccountResponse>>> Handle(GetAccountsByAccountHolderId request, CancellationToken cancellationToken)
    {
        var accounts = _unitOfWork.ReadRepositoryFor<Account>()
            .Entities
            .Where(a => a.AccountHolderId == request.AccountHolderId)
            .ToList();

        if(accounts.Count == 0)
        {
            return await Task.FromResult(new ResponseWrapper<List<AccountResponse>>().Failed("No accounts found for the specified account holder."));
        }

        return new ResponseWrapper<List<AccountResponse>>().Success(accounts.Adapt<List<AccountResponse>>(), "Accounts retrieved successfully");
    }
}
