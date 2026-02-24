using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ProjectName.Application.Mediator;

namespace ProjectName.Application.DependencyInjection;

public static class MediatorServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationMediator(this IServiceCollection services, Assembly assembly)
    {
        var handlerImplementations = assembly
            .DefinedTypes
            .Where(type => type is { IsAbstract: false, IsInterface: false })
            .ToList();

        foreach (var implementation in handlerImplementations)
        {
            var interfaces = implementation
                .GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>));

            foreach (var @interface in interfaces)
            {
                services.AddTransient(@interface, implementation.AsType());
            }
        }

        services.AddScoped<IMediator, ApplicationMediator>();
        return services;
    }
}
