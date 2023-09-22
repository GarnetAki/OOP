using Application.Dto;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Accounts;
using Presentation.Models.MessageSources;

namespace Presentation.Controllers;

public class MessageSourceController : Controller
{
    private readonly IMessageSourceService _service;

    public MessageSourceController(IMessageSourceService service)
    {
        _service = service;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    [Route("api/[controller]/CreateEmail")]
    public async Task<ActionResult<EmailDto>> CreateEmailAsync([FromBody] CreateEmailModel model)
    {
        EmailDto email = await _service.CreateEmailAsync(model.AccountIds, model.Email, CancellationToken);
        return Ok(email);
    }

    [HttpPost]
    [Route("api/[controller]/CreatePhone")]
    public async Task<ActionResult<PhoneDto>> CreatePhoneAsync([FromBody] CreatePhoneModel model)
    {
        PhoneDto phone = await _service.CreatePhoneAsync(model.AccountIds, model.Phone, CancellationToken);
        return Ok(phone);
    }

    [HttpPost]
    [Route("api/[controller]/RemovePhone")]
    public async Task<ActionResult<PhoneDto>> RemovePhoneAsync([FromBody] IdSourceModel model)
    {
        PhoneDto source = await _service.RemovePhoneAsync(model.Id, CancellationToken);
        return Ok(source);
    }

    [HttpPost]
    [Route("api/[controller]/RemoveEmail")]
    public async Task<ActionResult<EmailDto>> RemoveEmailAsync([FromBody] IdSourceModel model)
    {
        EmailDto source = await _service.RemoveEmailAsync(model.Id, CancellationToken);
        return Ok(source);
    }

    [HttpPost]
    [Route("api/[controller]/GetPhone")]
    public async Task<ActionResult<PhoneDto>> GetPhoneAsync([FromBody] IdSourceModel model)
    {
        PhoneDto phone = await _service.GetPhoneAsync(model.Id, CancellationToken);
        return Ok(phone);
    }

    [HttpPost]
    [Route("api/[controller]/GetEmail")]
    public async Task<ActionResult<EmailDto>> GetEmailAsync([FromBody] IdSourceModel model)
    {
        EmailDto phone = await _service.GetEmailAsync(model.Id, CancellationToken);
        return Ok(phone);
    }

    [HttpPost]
    [Route("api/[controller]/AddAccount")]
    public async Task<ActionResult<MessageSourceDto>> AddAccountAsync([FromBody] AddRemoveIdSourceModel model)
    {
        MessageSourceDto phone = await _service.AddAccountAsync(model.Id, model.OtherId, CancellationToken);
        return Ok(phone);
    }

    [HttpPost]
    [Route("api/[controller]/RemoveAccount")]
    public async Task<ActionResult<MessageSourceDto>> RemoveAccountAsync([FromBody] AddRemoveIdSourceModel model)
    {
        MessageSourceDto phone = await _service.RemoveAccountAsync(model.Id, model.OtherId, CancellationToken);
        return Ok(phone);
    }
}