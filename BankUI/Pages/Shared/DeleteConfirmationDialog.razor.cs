using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BankUI.Pages.Shared;

public partial class DeleteConfirmationDialog
{
    [Parameter]
    public string Message { get; set; }
    [CascadingParameter]
    IMudDialogInstance MudDialog { get; set; }
    private void Cancel() => MudDialog.Cancel();

    private void ConfirmDelete() => MudDialog.Close(DialogResult.Ok(true));
}
