using Common.Requests;
using Common.Responses;
using Common.Wrapper;

namespace BankUI.Services;

public interface IAccountService
{
    Task<ResponseWrapper<int>> AddAccountAsync(CreateAccount createAccount);
    Task<ResponseWrapper<int>> TransactionAsync(TransactionRequest transaction);
    Task<ResponseWrapper<AccountResponse>> GetAccountByIdAsync(int id);
    Task<ResponseWrapper<AccountResponse>> GetAccountByAccountNumberAsync(string accountNumber);
    Task<ResponseWrapper<List<AccountResponse>>> GetAccountsAsync();
    Task<ResponseWrapper<List<TransactionResponse>>> GetAccountTransactionsAsync(int accountId);
    Task<ResponseWrapper<List<AccountResponse>>> GetAccountsByAccountHolderIdAsync(int accountHolderId);
}
