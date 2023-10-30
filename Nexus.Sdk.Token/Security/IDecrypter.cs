namespace Nexus.Sdk.Token.Security
{
    public interface IDecrypter
    {
        public string DecryptString(string cipherText);
    }
}
