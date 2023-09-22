using DataAccess.Models;

namespace Presentation.Models.Messages;

public record CreateEmailMessageModel(Guid MessageSource, string Title, string Text, string Sender);