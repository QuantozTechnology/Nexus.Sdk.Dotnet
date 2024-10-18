namespace Nexus.Sdk.Shared.Http
{
    public interface IResponseHandler
    {
        public Task<T> HandleResponse<T>(HttpResponseMessage response, CancellationToken cancellationToken = default) where T : class;

        public Task HandleResponse(HttpResponseMessage response, CancellationToken cancellationToken = default);
    }
}
