using Application.Dto;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Users;

namespace Presentation.Controllers;

public class UserController : Controller
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    [Route("api/[controller]/Create")]
    public async Task<ActionResult<UserDto>> CreateAsync([FromBody] CreateUserModel model)
    {
        UserDto user = await _service.CreateUserAsync(model.Login, model.Password, model.ChiefId, model.AccountIds, CancellationToken);
        return Ok(user);
    }

    [HttpPost]
    [Route("api/[controller]/Remove")]
    public async Task<ActionResult<UserDto>> RemoveAsync([FromBody] IdUserModel model)
    {
        UserDto user = await _service.RemoveUserAsync(model.Id, CancellationToken);
        return Ok(user);
    }

    [HttpPost]
    [Route("api/[controller]/ChangeUser")]
    public async Task<ActionResult<UserDto>> ChangeUserAsync([FromBody] ChangeUserModel model)
    {
        UserDto user = await _service.ChangeUserAsync(model.Id, model.Login, model.Password, model.ChiefId, CancellationToken);
        return Ok(user);
    }

    [HttpPost]
    [Route("api/[controller]/AddSubordinateId")]
    public async Task<ActionResult<UserDto>> AddSubordinateIdAsync([FromBody] AddRemoveIdUserModel model)
    {
        UserDto user = await _service.AddSubordinateIdAsync(model.Id, model.OtherId, CancellationToken);
        return Ok(user);
    }

    [HttpPost]
    [Route("api/[controller]/AddAccountId")]
    public async Task<ActionResult<UserDto>> AddAccountIdAsync([FromBody] AddRemoveIdUserModel model)
    {
        UserDto user = await _service.AddAccountIdAsync(model.Id, model.OtherId, CancellationToken);
        return Ok(user);
    }

    [HttpPost]
    [Route("api/[controller]/RemoveAccountId")]
    public async Task<ActionResult<UserDto>> RemoveAccountIdAsync([FromBody] AddRemoveIdUserModel model)
    {
        UserDto user = await _service.RemoveAccountIdAsync(model.Id, model.OtherId, CancellationToken);
        return Ok(user);
    }

    [HttpPost]
    [Route("api/[controller]/Get")]
    public async Task<ActionResult<UserDto>> GetAsync([FromBody] IdUserModel model)
    {
        UserDto user = await _service.GetUserAsync(model.Id, CancellationToken);
        return Ok(user);
    }
}