using Common.Responses;
using MudBlazor;

namespace BankUI.Pages.Banking;

public partial class AccountHolderList
{
    public List<AccountHolderResponse> AccountHolders { get; set; } = [];
    private bool _loading = true;

    protected override async Task OnInitializedAsync()
    {
        var response = await _accountHolderService.GetAccountHoldersAsync();
        if (response.IsSuccessful)
        {
            AccountHolders = response.Data;
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

    private async Task AddAccountHolderAsync()
    {
        var parameters = new DialogParameters();
        var options = new DialogOptions
        {
            CloseButton = true,
            MaxWidth = MaxWidth.Medium,
            FullWidth = true,
            BackdropClick = true,
        };

        var dialog = await _dialogService.ShowAsync<AddAccountHolderDialog>("Add Account Holder", parameters, options);
        var result = await dialog.Result;
        if (!result.Canceled)
        {
            await OnInitializedAsync();
        }
    }
}
