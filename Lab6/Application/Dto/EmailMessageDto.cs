using DataAccess.Models;

namespace Application.Dto;

public record EmailMessageDto(Guid id, Guid messageSourceId, MessageStatus status, string title, string text, string sender);