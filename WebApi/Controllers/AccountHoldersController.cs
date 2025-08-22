using Application.Features.AccountHolders.Command;
using Common.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
public class AccountHoldersController : BaseApiController
{
    [HttpPost("add")]
    public async Task<IActionResult> AddAccountHolderAsync([FromBody] CreateAccountHolder createAccountHolder)
    {
        var result = await Sender.Send(new CreateAccountHolderCommand { CreateAccountHolder = createAccountHolder });
        if (result.IsSuccessful)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
}
