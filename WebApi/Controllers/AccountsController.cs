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
    public async Task<IActionResult> AddAccountAsync([FromBody]CreateAccount createAccount)
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

    [HttpGet("account-number/{accountNumber}")]
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

    [HttpPost("transaction")]
    public async Task<IActionResult> CreateTransactionAsync([FromBody] TransactionRequest transaction)
    {
        var command = new CreateTransactionCommand { Transaction = transaction };
        var response = await Sender.Send(command);
        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpGet("transactions/{accountId}")]
    public async Task<IActionResult> GetAccountTransactionsAsync(int accountId)
    {
        var query = new GetAccountTransactionsQuery { AccountId = accountId };
        var response = await Sender.Send(query);
        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return NotFound(response);
    }

    [HttpGet("account-holder/{accountHolderId}")]
    public async Task<IActionResult> GetAccountsByAccountHolderIdAsync(int accountHolderId)
    {
        var query = new GetAccountsByAccountHolderId { AccountHolderId = accountHolderId };
        var response = await Sender.Send(query);
        if (response.IsSuccessful)
        {
            return Ok(response);
        }
        return NotFound(response);
    }
}