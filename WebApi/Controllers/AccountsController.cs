using Application.Features.Accounts.Command;
using Application.Features.Accounts.Queries;
using Common.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : BaseApiController
{
    [HttpPost("add")]
    public async Task<IActionResult> AddAccountAsync([FromBody]CreateAccountRequest createAccount)
    {
        var command = new CreateAccountCommand { CreateAccount = createAccount };
        var response = await Sender.Send(command);

        if (response.IsSuccessful)        
        {
            return Ok(response);
        }

        return BadRequest("Account creation failed");
    }

    [HttpGet("id/{id}")]
    public async Task<IActionResult> GetAccountByIdAsync(int id)
    {
        var query = new GetAccountByIdQuery { Id = id };
        var response = await Sender.Send(query);
        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return NotFound(response);
    }

    [HttpGet("accountNumber/{accountNumber}")]
    public async Task<IActionResult> GetAccountByAccountNumberAsync(string accountNumber)
    {
        var query = new GetAccountByAccountNumberQuery { AccountNumber = accountNumber };
        var response = await Sender.Send(query);
        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return NotFound(response);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllAccountsAsync()
    {
        var query = new GetAccountsQuery();
        var response = await Sender.Send(query);
        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return NotFound(response);
    }
}