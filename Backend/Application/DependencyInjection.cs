using Application.Abstractions;
using Application.Behaviors;
using Application.Hubs;
using Application.Users.Services;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblyContaining<ApplicationAssemblyReference>();
        });

        services.AddSignalR();
        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));

        services.AddValidatorsFromAssemblyContaining<ApplicationAssemblyReference>();

        services.AddScoped<IAuthService,AuthService>();
        services.AddSingleton<IUserIdProvider, AppUserIdProvider>();

        return services;
    }
}
