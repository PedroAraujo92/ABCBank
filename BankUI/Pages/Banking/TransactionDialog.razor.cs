using Common.Requests;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BankUI.Pages.Banking;

public partial class TransactionDialog
{
    [Parameter]
    public int AccountId { get; set; }
    [Parameter]
    public decimal Balance { get; set; }
    [CascadingParameter]
    IMudDialogInstance MudDialog { get; set; }
    public TransactionRequest TransactionRequest { get; set; } = new();
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
        TransactionRequest.AccountId = AccountId;
        var response = await _accountService.TransactionAsync(TransactionRequest);
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

    void Cancel() => MudDialog.Cancel();
}
