using Domain.Groups;
using Domain.Messages;
using Domain.Tokens;
using Domain.Users;
using Infrastructure.Factories;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {

        services.AddSingleton(provider =>
        {
            var configuration = provider.GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString("Default") ??
                throw new ApplicationException("Connection string is null");

            return new SqlConnectionFactory(connectionString);
        });

        services.AddScoped<IGroupRepository,GroupRepository>();
        services.AddScoped<IUserRepository,UserRepository>();
        services.AddScoped<IMessageRepository,MessageRepository>();
        services.AddScoped<ITokenRepository,TokenRepository>();
        return services;
    }
}
