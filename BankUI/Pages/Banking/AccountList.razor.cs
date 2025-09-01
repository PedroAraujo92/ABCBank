using Common.Responses;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BankUI.Pages.Banking;
public partial class AccountList
{
    [Parameter]
    public int AccountHolderId { get; set; }
    public List<AccountResponse> Accounts { get; set; } = [];
    private bool _loading = true;
    protected override async Task OnInitializedAsync()
    {
        var response = await _accountService.GetAccountsByAccountHolderIdAsync(AccountHolderId);
        if (response.IsSuccessful)
        {
            Accounts = response.Data;
        }
        else
        {
            foreach (var error in response.Messages)
            {
                _snackbar.Add(error, Severity.Error);
            }
        }

        _loading = false;
    }

    private async Task AddAccountAsync()
    {
        var parameters = new DialogParameters
        {
            {nameof(AddAccountDialog.AccountHolderId), AccountHolderId }
        };
        var options = new DialogOptions
        {
            CloseButton = true,
            MaxWidth = MaxWidth.Small,
            FullWidth = true,
            BackdropClick = false,
        };
        var dialog = await _dialogService.ShowAsync<AddAccountDialog>("Open Bank Account", parameters, options);
        var result = await dialog.Result;
        if (!result.Canceled)
        {
            await OnInitializedAsync();
        }
    }
}
