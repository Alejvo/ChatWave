using Domain.Friends;
using Domain.Groups;
using Domain.Messages;
using Domain.Tokens;
using Domain.Users;
using Infrastructure.ReadDatabase;
using Infrastructure.WriteDatabase.Factories;
using Infrastructure.WriteDatabase.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared;

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
        services.AddScoped<IFriendRepository,FriendRepository>();

        services.AddScoped<IEventStore,EventStore>();

        return services;
    }
}
