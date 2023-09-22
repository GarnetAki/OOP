namespace Presentation.Models.Users;

public record CreateUserModel(string Login, string Password, Guid ChiefId, ICollection<Guid> AccountIds);