using Common.Responses;
using Microsoft.AspNetCore.Components;

namespace BankUI.Pages.Banking;

public partial class TransactionHistoryList
{
    [Parameter]
    public int AccountId { get; set; }
    public List<TransactionResponse> Transactions { get; set; } = [];
    public AccountResponse Account { get; set; } = new();

    private bool _loading = true;

    protected override async Task OnInitializedAsync()
    {
        _loading = true;
        await GetAccountDetailsAsync();
        await LoadTransactionsAsync();
        _loading = false;
    }

    private async Task GetAccountDetailsAsync()
    {
        var response = await _accountService.GetAccountByIdAsync(AccountId);
        if (response.IsSuccessful)
        {
            Account = response.Data;
        }
        else
        {
            foreach (var error in response.Messages)
            {
                _snackbar.Add(error, MudBlazor.Severity.Error);
            }
        }
    }

    private async Task LoadTransactionsAsync()
    {
        var response = await _accountService.GetAccountTransactionsAsync(AccountId);
        if (response.IsSuccessful)
        {
            Transactions = response.Data.ToList();
        }
        else
        {
            foreach (var error in response.Messages)
            {
                _snackbar.Add(error, MudBlazor.Severity.Error);
            }
        }
    }
    private void PageClosed()
    {
        _navigationManager.NavigateTo($"/banking/manage-accounts/{Account.AccountHolderId}");
    }
}
