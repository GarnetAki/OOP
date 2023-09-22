using Application.Dto;
using DataAccess.Models;

namespace Application.Mapping;

public static class UserMapping
{
    public static UserDto AsDto(this User user)
        => new UserDto(user.Id, user.Login, user.Password, user.ChiefId, user.Subordinates.Select(a => a.Id).ToList(), user.Accounts.Select(a => a.Id).ToList(), user.Reports.Select(a => a.Id).ToList());
}