namespace Presentation.Models.MessageSources;

public record CreatePhoneModel(ICollection<Guid> AccountIds, string Phone);