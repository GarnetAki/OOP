using Application.Dto;

namespace Application.Services;

public interface IWorkService
{
    Task<Guid> LogInAsync(string login, string password, CancellationToken cancellationToken);

    Task<List<AccountDto>> GetListOfAccountsAsync(Guid id, CancellationToken cancellationToken);

    Task<List<MessageSourceDto>> GetListOfSourcesAsync(Guid id, CancellationToken cancellationToken);

    Task<List<MessageDto>> GetMessagesAsync(Guid id, CancellationToken cancellationToken);

    Task<string> GetMessageTextAsync(Guid id, Guid messageId, CancellationToken cancellationToken);

    Task<MessageDto> ChangeStatusToProcessedAsync(Guid id, Guid messageId, CancellationToken cancellationToken);

    Task<MessageDto> ChangeStatusToReceivedAsync(Guid id, Guid messageId, CancellationToken cancellationToken);

    Task<ReportDto> MakeReportAsync(Guid id, CancellationToken cancellationToken);

    Task<List<ReportDto>> GetReportsAsync(Guid id, CancellationToken cancellationToken);
}