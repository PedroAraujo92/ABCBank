using Common.Responses;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BankUI.Pages.Banking;

public partial class ManageAccounts
{
    [Parameter]
    public int AccountHolderId { get; set; }
    public AccountHolderResponse AccountHolder { get; set; } = new();
    private bool _loading = true;
    protected override async Task OnInitializedAsync()
    {
        var response = await _accountHolderService.GetAccountHolderByIdAsync(AccountHolderId);
        if (response.IsSuccessful)
        {
            AccountHolder = response.Data;
        }
        else
        {
            foreach (var error in response.Messages)
            {
                _snackbar.Add(error, Severity.Error);
            }
            _navigationManager.NavigateTo("/banking/account-holder-list");
        }
        _loading = false;
    }

    private void PageClosed()
    {
        _navigationManager.NavigateTo("/banking/account-holder-list");
    }

}
