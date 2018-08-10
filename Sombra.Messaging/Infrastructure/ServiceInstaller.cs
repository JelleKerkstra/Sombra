using System;
using System.Diagnostics;
using System.Reflection;
using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using Sombra.Core;
using Sombra.Messaging.DependencyValidation;

namespace Sombra.Messaging.Infrastructure
{
    public static class ServiceInstaller
    {
        public const string LoggingQueue = "IMessageLoggingQueue";
        public const string ExceptionQueue = "ExceptionQueue";
        public const string WebRequestsQueue = "WebRequestsQueue";

        public static ServiceProvider Run(Assembly assembly, string busConnectionString, Func<IServiceCollection, IServiceCollection> addAdditionalServices = null, params Action<ServiceProvider>[] additionalActions)
        {
            var installerStopwatch = new Stopwatch();
            installerStopwatch.Start();

            addAdditionalServices = addAdditionalServices ?? (s => s);

            var serviceProvider = addAdditionalServices(new ServiceCollection())
                .AddEventHandlers(assembly)
                .AddRequestHandlers(assembly)
                .AddPingRequestHandlers(assembly)
                .AddRabbitBus(busConnectionString)
                .BuildServiceProvider(true);

            ExtendedConsole.Log("Services are registered.");

            Logger.SetServiceProvider(serviceProvider);
            ExtendedConsole.Log("Logger initialized.");

            var bus = serviceProvider.GetRequiredService<IBus>();

            var responder = new AutoResponder(bus, new AutoResponderRequestDispatcher(serviceProvider));
            responder.RespondAsync(assembly);
            ExtendedConsole.Log("AutoResponders initialized.");

            var pingResponder = new PingResponder(bus, new AutoResponderRequestDispatcher(serviceProvider));
            pingResponder.RespondAsync(assembly);
            ExtendedConsole.Log("PingResponders initialized.");

            var subscriber = new CustomAutoSubscriber(bus, new CustomAutoSubscriberMessageDispatcher(serviceProvider), assembly.FullName);
            subscriber.SubscribeAsync(assembly);
            ExtendedConsole.Log("AutoSubscribers initialized.");

            if(additionalActions != null)
            {
                foreach (var additionalAction in additionalActions)
                {
                    var additionalActionStopwatch = new Stopwatch();
                    additionalActionStopwatch.Start();

                    ExtendedConsole.Log($"Running {additionalAction.Method.Name}");
                    additionalAction(serviceProvider);
                    additionalActionStopwatch.Stop();

                    ExtendedConsole.Log($"{additionalAction.Method.Name} finished running in {additionalActionStopwatch.ElapsedMilliseconds}ms.");
                }
            }

            installerStopwatch.Stop();
            ExtendedConsole.Log($"Finished running in {installerStopwatch.ElapsedMilliseconds}ms.");

            return serviceProvider;
        }
    }
}