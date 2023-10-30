namespace Nexus.Sdk.Shared.ErrorHandling
{
    public class ApiException : Exception
    {
        public int StatusCode;

        public ApiException(int statusCode, string reasonPhrase) : base(reasonPhrase)
        {
            StatusCode = statusCode;
        }
    }
}
