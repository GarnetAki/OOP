using DataAccess.Models;

namespace Application.Dto;

public record UserDto(Guid id, string login, string password, Guid chiefId, ICollection<Guid> subordinateIds, ICollection<Guid> accountIds, IReadOnlyCollection<Guid> reportIds);