using Application.Dto;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Accounts;

namespace Presentation.Controllers;

public class AccountController : Controller
{
    private readonly IAccountService _service;

    public AccountController(IAccountService service)
    {
        _service = service;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    [Route("api/[controller]/Create")]
    public async Task<ActionResult<AccountDto>> CreateAsync([FromBody] CreateAccountModel model)
    {
        AccountDto account = await _service.CreateAccountAsync(model.UserIds, model.MessageSourceIds, CancellationToken);
        return Ok(account);
    }

    [HttpPost]
    [Route("api/[controller]/Remove")]
    public async Task<ActionResult<AccountDto>> RemoveAccount([FromBody] IdAccountModel model)
    {
        AccountDto account = await _service.RemoveAccountAsync(model.Id, CancellationToken);
        return Ok(account);
    }

    [HttpPost]
    [Route("api/[controller]/AddUser")]
    public async Task<ActionResult<AccountDto>> AddUserAsync([FromBody] AddRemoveIdAccountModel model)
    {
        AccountDto account = await _service.AddUserAsync(model.Id, model.OtherId, CancellationToken);
        return Ok(account);
    }

    [HttpPost]
    [Route("api/[controller]/AddMessageSource")]
    public async Task<ActionResult<AccountDto>> AddMessageSourceAsync([FromBody] AddRemoveIdAccountModel model)
    {
        AccountDto account = await _service.AddMessageSourceAsync(model.Id, model.OtherId, CancellationToken);
        return Ok(account);
    }

    [HttpPost]
    [Route("api/[controller]/RemoveUser")]
    public async Task<ActionResult<AccountDto>> RemoveUser([FromBody] AddRemoveIdAccountModel model)
    {
        AccountDto account = await _service.RemoveUserAsync(model.Id, model.OtherId, CancellationToken);
        return Ok(account);
    }

    [HttpPost]
    [Route("api/[controller]/RemoveMessageSource")]
    public async Task<ActionResult<AccountDto>> RemoveMessageSourceAsync([FromBody] AddRemoveIdAccountModel model)
    {
        AccountDto account = await _service.RemoveMessageSourceAsync(model.Id, model.OtherId, CancellationToken);
        return Ok(account);
    }

    [HttpPost]
    [Route("api/[controller]/Get")]
    public async Task<ActionResult<AccountDto>> GetAsync([FromBody] IdAccountModel model)
    {
        AccountDto account = await _service.GetAccountAsync(model.Id, CancellationToken);
        return Ok(account);
    }
}