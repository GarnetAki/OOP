namespace Presentation.Models.Users;

public record ChangeUserModel(Guid Id, string Login, string Password, Guid ChiefId);