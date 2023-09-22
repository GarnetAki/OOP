namespace Application.Dto;

public record MessageSourceDto(Guid id, ICollection<Guid> accountIds, ICollection<Guid> messageIds);