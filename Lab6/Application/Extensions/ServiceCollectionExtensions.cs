using Application.Services;
using Application.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<IUserService, UserService>();
        collection.AddScoped<IAccountService, AccountService>();
        collection.AddScoped<IMessageSourceService, MessageSourceService>();
        collection.AddScoped<IMessageService, MessageService>();
        collection.AddScoped<IWorkService, WorkService>();

        return collection;
    }
}