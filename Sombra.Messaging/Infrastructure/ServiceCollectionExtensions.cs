using System;
using System.Linq;
using System.Reflection;
using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using Sombra.Core;
using Sombra.Core.Extensions;

namespace Sombra.Messaging.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRabbitBus(this IServiceCollection serviceCollection, string connectionString)
        {
            var bus = RabbitHutch.CreateBus(connectionString).WaitForConnection();
            ExtendedConsole.Log($"Bus connected: {bus.IsConnected}.");

            return serviceCollection.AddSingleton(bus);
        }

        public static IServiceCollection AddEventHandlers(this IServiceCollection serviceCollection, Assembly assembly)
        {
            return serviceCollection.AddGenericInterfaceType(assembly, typeof(IAsyncEventHandler<>));
        }

        public static IServiceCollection AddRequestHandlers(this IServiceCollection serviceCollection, Assembly assembly)
        {
            return serviceCollection.AddGenericInterfaceType(assembly, typeof(IAsyncRequestHandler<,>));
        }

        private static IServiceCollection AddGenericInterfaceType(this IServiceCollection serviceCollection, Assembly assembly, Type type)
        {
            if (!type.IsInterface || !type.IsGenericType) throw new ArgumentException($"The supplied type must be a generic interface. Current type: {type}", nameof(type));

            return assembly.GetGenericInterfaceTypes(type).Aggregate(serviceCollection, (current, t) => current.AddTransient(t));
        }
    }
}