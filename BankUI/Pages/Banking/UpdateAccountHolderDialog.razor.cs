using BankUI.Pages.Banking.Validators;
using Common.Requests;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BankUI.Pages.Banking;

public partial class UpdateAccountHolderDialog
{
    private UpdateAccountHolderValidator _validator = new();

    [Parameter]
    public UpdateAccountHolder UpdateAccountHolderRequest { get; set; } = new();

    [CascadingParameter]
    IMudDialogInstance MudDialog { get; set; }

    MudForm _form = default;

    private void Cancel() => MudDialog.Cancel();
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
        await _form.Validate();
        if (_form.IsValid)
        {
            var response = await _accountHolderService.UpdateAccountHolderAsync(UpdateAccountHolderRequest);
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
    }
}
