using Application.Repositories;
using Common.Requests;
using Common.Wrapper;
using Domain;
using MediatR;

namespace Application.Features.AccountHolders.Command;

public class UpdateAccountHolderCommand : IRequest<ResponseWrapper<int>>
{
    public UpdateAccountHolder UpdateAccountHolder { get; set; }
}

public class UpdateAccountHolderCommandHandler(IUnitOfWork<int> unitOfWork)
    : IRequestHandler<UpdateAccountHolderCommand, ResponseWrapper<int>>
{
    private readonly IUnitOfWork<int> _unitOfWork = unitOfWork;
    public async Task<ResponseWrapper<int>> Handle(UpdateAccountHolderCommand request, CancellationToken cancellationToken)
    {
        var accountHolderInDb = await _unitOfWork.ReadRepository<AccountHolder>().GetByIdAsync(request.UpdateAccountHolder.Id);
        if (accountHolderInDb == null)
        {
            return new ResponseWrapper<int>().Failed("Account holder not found.");
        }

        var updatedAccountHolder = accountHolderInDb.Update(request.UpdateAccountHolder.FirstName,
            request.UpdateAccountHolder.LastName, 
            request.UpdateAccountHolder.ContactNumber, 
            request.UpdateAccountHolder.EmailAddress);

        await _unitOfWork.WriteRepositoryFor<AccountHolder>().UpdateAsync(accountHolderInDb);
        await _unitOfWork.CommitAsync(cancellationToken);
        return new ResponseWrapper<int>()
            .Success(accountHolderInDb.Id, "Account holder updated successfully.");
    }
}
