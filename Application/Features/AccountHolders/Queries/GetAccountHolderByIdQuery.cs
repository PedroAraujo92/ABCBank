using Application.Repositories;
using Common.Responses;
using Common.Wrapper;
using Domain;
using Mapster;
using MediatR;

namespace Application.Features.AccountHolders.Queries;

public class GetAccountHolderByIdQuery:IRequest<ResponseWrapper<AccountHolderResponse>>
{
    public int Id { get; set; }
}

public class GetAccountHolderByIdQueryHandler(IUnitOfWork<int> unitOfWork)
    : IRequestHandler<GetAccountHolderByIdQuery, ResponseWrapper<AccountHolderResponse>>
{
    private readonly IUnitOfWork<int> _unitOfWork = unitOfWork;
    public async Task<ResponseWrapper<AccountHolderResponse>> Handle(GetAccountHolderByIdQuery request, CancellationToken cancellationToken)
    {
        var accountHolder = await _unitOfWork.ReadRepositoryFor<AccountHolder>().GetByIdAsync(request.Id);
        if (accountHolder == null)
        {
            return new ResponseWrapper<AccountHolderResponse>().Failed("Account holder not found.");
        }
        var response = accountHolder.Adapt<AccountHolderResponse>();
        return new ResponseWrapper<AccountHolderResponse>().Success(response, "Account holder retrieved successfully.");
    }
}
