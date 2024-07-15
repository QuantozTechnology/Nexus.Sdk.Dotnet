
namespace Nexus.Sdk.Shared.ErrorHandling
{
    public class CustomErrorsException : NexusApiException
    {
        public CustomErrors CustomErrors = new();

        public CustomErrorsException(int statusCode, string reasonPhrase, string[]? errorCodes) : base(statusCode, reasonPhrase, errorCodes)
        {
            if (errorCodes != null)
            {
                foreach (var errorCode in errorCodes)
                {
                    CustomErrors.AddError(new CustomError(statusCode.ToString(), errorCode, null));
                }
            }
        }

        public override string ToString()
        {
            return CustomErrors.ToString();
        }
    }
}
