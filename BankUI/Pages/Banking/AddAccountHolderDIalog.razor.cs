using BankUI.Pages.Banking.Validators;
using Common.Requests;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BankUI.Pages.Banking;

public partial class AddAccountHolderDialog
{
    private CreateAccountHolderValidator _validator = new();

    [Parameter]
    public CreateAccountHolder CreateAccountHolderRequest { get; set; } = new();

    [CascadingParameter]
    IMudDialogInstance MudDialog { get; set; }

    MudForm _form = default;

    private async Task SubmitAsync()
    {
        await _form.Validate();
        if (_form.IsValid)
        {
            await SaveAsync();
        }
    }

    private async Task SaveAsync()
    {
        var response = await _accountHolderService.AddAccountHolderAsync(CreateAccountHolderRequest);
        if (!response.IsSuccessful)
        {
            foreach (var error in response.Messages)
            {
                _snackbar.Add(error, Severity.Error);
            }
            return;
        }

        _snackbar.Add(response.Messages[0], Severity.Success);
        MudDialog.Close(DialogResult.Ok(true));
    }

    private void Cancel() => MudDialog.Cancel();
}
