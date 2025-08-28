using Common.Requests;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BankUI.Pages.Banking;

public partial class AddAccountHolderDialog
{
    [Parameter]
    public CreateAccountHolder CreateAccountHolderRequest { get; set; } = new();

    [CascadingParameter]
    IMudDialogInstance MudDialog { get; set; }

    MudForm _form = default;

    public DateTime? DateOfBirth { get; set; }

    private async Task SaveAsync()
    {
        await _form.Validate();
        if (_form.IsValid)
        {
            // Cast DateOfBirth to DateOnly and assign to CreateAccountHolderRequest
            if (!DateOfBirth.HasValue)
            {
                _snackbar.Add("Date of Birth is required.", Severity.Error);
                return;
            }
            CreateAccountHolderRequest.DateOfBirth = (DateTime)DateOfBirth;

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
    }

    private void Cancel() => MudDialog.Cancel();
}
