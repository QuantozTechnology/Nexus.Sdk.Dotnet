namespace Nexus.Token.SDK.Security
{
    public interface IDecrypter
    {
        public string DecryptString(string cipherText);
    }
}
