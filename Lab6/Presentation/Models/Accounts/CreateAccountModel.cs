namespace Presentation.Models.Accounts;

public record CreateAccountModel(ICollection<Guid> UserIds, ICollection<Guid> MessageSourceIds);