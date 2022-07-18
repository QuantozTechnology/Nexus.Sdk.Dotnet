namespace Nexus.Token.SDK.Security
{
    public interface IEncrypter
    {
        public string EncryptString(string plainText);
    }
}
