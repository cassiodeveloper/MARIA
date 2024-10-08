using Maria.Application.Accounts;
using Maria.Core.Accounts;
using Maria.Infrastructure.Database;

namespace Maria.Rest.Infraestructure;

public static class AddServicesExtension
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddSingleton<IPasswordService, PasswordService>();
        services.AddSingleton<ITokenService, TokenService>();
        services.AddScoped<IAccountsService, AccountsService>();
        services.AddScoped<IUsersRepository, UsersRepository>();

        return services;
    }
}
