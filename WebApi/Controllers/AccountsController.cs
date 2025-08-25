using Application.Features.Accounts.Command;
using Common.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : BaseApiController
{
    [HttpPost]
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
}