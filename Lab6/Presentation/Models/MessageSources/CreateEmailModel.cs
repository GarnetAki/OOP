namespace Presentation.Models.MessageSources;

public record CreateEmailModel(ICollection<Guid> AccountIds, string Email);