namespace Sombra.Messaging.Infrastructure
{
    public static class ResponseExtensions
    {
        public static TResponse RequestFailed<TResponse>(this TResponse response)
            where TResponse : IResponse
        {
            response.IsRequestSuccessful = false;
            return response;
        }
    }
}