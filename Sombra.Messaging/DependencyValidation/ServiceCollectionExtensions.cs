using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Sombra.Core.Extensions;
using Sombra.Messaging.Infrastructure;

namespace Sombra.Messaging.DependencyValidation
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPingRequestHandlers(this IServiceCollection serviceCollection, Assembly assembly)
        {
            return assembly.GetGenericInterfaceTypes(typeof(IAsyncRequestHandler<,>)).Aggregate(serviceCollection,
                (current, t) =>
                {
                    var genericArguments = t.GetInterface(typeof(IAsyncRequestHandler<,>).Name).GetGenericArguments();
                    var requestType = genericArguments[0];
                    var responseType = genericArguments[1];

                    var pingRequestType = typeof(Ping<,>).MakeGenericType(requestType, responseType);
                    var pingHandlerType = typeof(PingRequestHandler<,>).MakeGenericType(pingRequestType, typeof(PingResponse));

                    return current.AddTransient(pingHandlerType);
                });
        }
    }
}