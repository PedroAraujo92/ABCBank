using Application.Features.AccountHolders.Command;
using Application.Features.AccountHolders.Queries;
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

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAccountHolderAsync([FromBody] UpdateAccountHolder updateAccountHolder)
    {
        var result = await Sender.Send(new UpdateAccountHolderCommand { UpdateAccountHolder = updateAccountHolder });
        if (result.IsSuccessful)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteAccountHolderAsync(int id)
    {
        var result = await Sender.Send(new DeleteAccountHolderCommand { Id = id });
        if (result.IsSuccessful)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpGet("id/{id}")]
    public async Task<IActionResult> GetAccountHolderAsync(int id)
    {
        var result = await Sender.Send(new GetAccountHolderByIdQuery { Id = id });
        if (result.IsSuccessful)
        {
            return Ok(result);
        }
        return NotFound(result);
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAccountHoldersAsync()
    {
        var result = await Sender.Send(new GetAccountHoldersQuery ());
        if (result.IsSuccessful)
        {
            return Ok(result);
        }
        return NotFound(result);
    }
}
