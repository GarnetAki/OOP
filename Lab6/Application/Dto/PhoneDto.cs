using DataAccess.Models;

namespace Application.Dto;

public record PhoneDto(Guid id, ICollection<Guid> accounts, string phoneNumber, ICollection<Guid> messages);