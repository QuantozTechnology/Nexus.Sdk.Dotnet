namespace Nexus.Sdk.Token.Security
{
    public interface IEncrypter
    {
        public string EncryptString(string plainText);
    }
}
