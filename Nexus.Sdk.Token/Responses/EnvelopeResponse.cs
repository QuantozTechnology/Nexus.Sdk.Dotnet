namespace Nexus.Sdk.Token.Responses;

public class EnvelopeResponse
{
    public string Code { get; set; }
    public string hash { get; set; }
    public string envelope { get; set; }
    public string status { get; set; }
    public string type { get; set; }
    public string created { get; set; }
    public string validUntil { get; set; }
    public string memo { get; set; }
    public string message { get; set; }
}
