using DataAccess.Models;
namespace Application.Dto;

public record AccountDto(Guid id, ICollection<Guid> userIds, ICollection<Guid> messageSourceIds);