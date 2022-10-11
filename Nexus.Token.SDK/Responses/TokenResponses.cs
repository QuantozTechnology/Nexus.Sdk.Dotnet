using System.Text.Json.Serialization;

namespace Nexus.Token.SDK.Responses;

public class TokenResponse
{
    [JsonPropertyName("code")]
    public string Code { get; }

    [JsonPropertyName("name")]
    public string Name { get; }

    [JsonPropertyName("issuerAddress")]
    public string IssuerAddress { get; }

    [JsonPropertyName("status")]
    public string Status { get; }

    [JsonPropertyName("created")]
    public string Created { get; }

    [JsonPropertyName("blockchainId")]
    public string BlockchainId { get; }

    [JsonConstructor]
    public TokenResponse(string code, string name, string issuerAddress, string status, string created, string blockchainId)
    {
        Code = code;
        Name = name;
        IssuerAddress = issuerAddress;
        Status = status;
        Created = created;
        BlockchainId = blockchainId;
    }
}

public class CreateTokenResponse
{
    [JsonPropertyName("tokens")]
    public TokenResponse[] Tokens { get; }

    [JsonConstructor]
    public CreateTokenResponse(TokenResponse[] tokens)
    {
        Tokens = tokens;
    }

}
