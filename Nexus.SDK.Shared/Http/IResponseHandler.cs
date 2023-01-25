namespace Nexus.SDK.Shared.Http
{
    public interface IResponseHandler
    {
        public Task<T> HandleResponse<T>(HttpResponseMessage response) where T : class;

        public Task HandleResponse(HttpResponseMessage response);
    }
}
