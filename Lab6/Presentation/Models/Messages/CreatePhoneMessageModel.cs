using DataAccess.Models;

namespace Presentation.Models.Messages;

public record CreatePhoneMessageModel(Guid MessageSource, string Text, string Sender);