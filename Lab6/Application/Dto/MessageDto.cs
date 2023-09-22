using DataAccess.Models;

namespace Application.Dto;

public record MessageDto(Guid id, Guid messageSourceId, MessageStatus status);