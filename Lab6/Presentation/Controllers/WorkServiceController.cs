using Application.Dto;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Messages;
using Presentation.Models.Work;

namespace Presentation.Controllers;

public class WorkServiceController : Controller
{
    private readonly IWorkService _service;

    public WorkServiceController(IWorkService service)
    {
        _service = service;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    [Route("api/[controller]/LogIn")]
    public async Task<ActionResult<Guid>> LogInAsync([FromBody] AuthorizeWorkModel model)
    {
        Guid message = await _service.LogInAsync(model.Login, model.Password, CancellationToken);
        return Ok(message);
    }

    [HttpPost]
    [Route("api/[controller]/GetListOfAccounts")]
    public async Task<ActionResult<List<AccountDto>>> GetListOfAccountsAsync([FromBody] IdWorkModel model)
    {
        List<AccountDto> accounts = await _service.GetListOfAccountsAsync(model.Id, CancellationToken);
        return Ok(accounts);
    }

    [HttpPost]
    [Route("api/[controller]/GetListOfSources")]
    public async Task<ActionResult<List<MessageSourceDto>>> GetListOfSourcesAsync([FromBody] IdWorkModel model)
    {
        List<MessageSourceDto> sources = await _service.GetListOfSourcesAsync(model.Id, CancellationToken);
        return Ok(sources);
    }

    [HttpPost]
    [Route("api/[controller]/GetMessages")]
    public async Task<ActionResult<List<MessageDto>>> GetMessagesAsync([FromBody] IdWorkModel model)
    {
        List<MessageDto> messages = await _service.GetMessagesAsync(model.Id, CancellationToken);
        return Ok(messages);
    }

    [HttpPost]
    [Route("api/[controller]/GetMessageText")]
    public async Task<ActionResult<string>> GetMessageTextAsync([FromBody] TwoIdWorkModel model)
    {
        string message = await _service.GetMessageTextAsync(model.Id, model.OtherId, CancellationToken);
        return Ok(message);
    }

    [HttpPost]
    [Route("api/[controller]/ChangeStatusToProcessed")]
    public async Task<ActionResult<MessageDto>> ChangeStatusToProcessedAsync([FromBody] TwoIdWorkModel model)
    {
        MessageDto message = await _service.ChangeStatusToProcessedAsync(model.Id, model.OtherId, CancellationToken);
        return Ok(message);
    }

    [HttpPost]
    [Route("api/[controller]/ChangeStatusToReceived")]
    public async Task<ActionResult<MessageDto>> ChangeStatusToReceivedAsync([FromBody] TwoIdWorkModel model)
    {
        MessageDto message = await _service.ChangeStatusToReceivedAsync(model.Id, model.OtherId, CancellationToken);
        return Ok(message);
    }

    [HttpPost]
    [Route("api/[controller]/MakeReport")]
    public async Task<ActionResult<ReportDto>> MakeReportAsync([FromBody] IdWorkModel model)
    {
        ReportDto report = await _service.MakeReportAsync(model.Id, CancellationToken);
        return Ok(report);
    }

    [HttpPost]
    [Route("api/[controller]/GetReports")]
    public async Task<ActionResult<List<ReportDto>>> GetReportsAsync([FromBody] IdWorkModel model)
    {
        List<ReportDto> report = await _service.GetReportsAsync(model.Id, CancellationToken);
        return Ok(report);
    }
}