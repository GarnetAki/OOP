using DataAccess.Models;

namespace Application.Dto;

public record EmailDto(Guid id, ICollection<Guid> accountIds, string emailAddress, ICollection<Guid> messageIds);