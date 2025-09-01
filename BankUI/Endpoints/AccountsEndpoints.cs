namespace BankUI.Endpoints;

public static class AccountsEndpoints
{
    public const string Add = "/api/accounts/add";
    public const string Transaction = "/api/accounts/transaction";
    public const string GetAll = "/api/accounts/all";

    public static string GetById(int id) => $"/api/accounts/id/{id}";
    public static string GetByAccountNumber(string accountNumber) => $"/api/accounts/account-number/{accountNumber}";
    public static string GetTransactionsByAccountById(int id) => $"/api/accounts/transactions/{id}";
    public static string GetAccountsByAccountHolderId(int accountHolderId) => $"/api/accounts/account-holder/{accountHolderId}";
}
