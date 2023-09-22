using Application.Dto;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Messages;

namespace Presentation.Controllers;

public class MessageController : Controller
{
    private readonly IMessageService _service;

    public MessageController(IMessageService service)
    {
        _service = service;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    [Route("api/[controller]/CreateEmailMessage")]
    public async Task<ActionResult<EmailMessageDto>> CreateEmailMessageAsync([FromBody] CreateEmailMessageModel model)
    {
        EmailMessageDto email = await _service.CreateEmailMessageAsync(model.MessageSource, model.Title, model.Text, model.Sender, CancellationToken);
        return Ok(email);
    }

    [HttpPost]
    [Route("api/[controller]/CreatePhoneMessage")]
    public async Task<ActionResult<PhoneMessageDto>> CreatePhoneMessageAsync([FromBody] CreatePhoneMessageModel model)
    {
        PhoneMessageDto phone = await _service.CreatePhoneMessageAsync(model.MessageSource, model.Text, model.Sender, CancellationToken);
        return Ok(phone);
    }
}