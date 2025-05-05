using System.Reflection;
using CqrsMapLibrary.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using CqrsMapLibrary.Dispatchers;

namespace CqrsMapLibrary.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCqrs(this IServiceCollection services, Assembly[] assemblies)
    {
        services.AddScoped<IDispatcher, Dispatcher>();

        var handlerTypes = assemblies
            .SelectMany(a => a.GetTypes())
            .Where(t => !t.IsAbstract && !t.IsInterface)
            .SelectMany(t => t.GetInterfaces()
                .Where(i => i.IsGenericType &&
                            (i.GetGenericTypeDefinition() == typeof(ICommandHandler<,>) ||
                             i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>)))
                .Select(i => new { Service = i, Implementation = t }));

        foreach (var handler in handlerTypes)
        {
            services.AddTransient(handler.Service, handler.Implementation);
        }

        return services;
    }
}