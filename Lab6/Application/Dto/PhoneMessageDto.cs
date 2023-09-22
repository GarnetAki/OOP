using DataAccess.Models;

namespace Application.Dto;

public record PhoneMessageDto(Guid id, Guid messageSource, MessageStatus status, string text, string sender);