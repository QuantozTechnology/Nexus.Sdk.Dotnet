using Nexus.Sdk.Shared.Responses;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Nexus.Sdk.Shared.Requests;

public class CreateMailRequest
{
    [JsonPropertyName("type")]
    [Required]
    public required string Type { get; set; }

    [JsonPropertyName("references")]
    [Required]
    public required MailEntityCodes References { get; set; }

    [JsonPropertyName("content")]
    public MailContent? Content { get; set; }

    [JsonPropertyName("recipient")]
    [Required]
    public required PostMailRecipient Recipient { get; set; }
}

public class PostMailRecipient
{
    private const string commaSeperatedListOfEmailRegex = "^(\\s?[^\\s,]+@[^\\s,]+\\.[^\\s,]+\\s?,)*(\\s?[^\\s,]+@[^\\s,]+\\.[^\\s,]+)$";

    [JsonPropertyName("email")]
    [EmailAddress(ErrorMessage = "InvalidEmailAddress")]
    [StringLength(80)]
    [Required]
    public required string Email { get; set; }

    [JsonPropertyName("cc")]
    [RegularExpression(commaSeperatedListOfEmailRegex, ErrorMessage = "InvalidCCEmailAddress")]
    [StringLength(80)]
    public string? CC { get; set; }

    [JsonPropertyName("bcc")]
    [RegularExpression(commaSeperatedListOfEmailRegex, ErrorMessage = "InvalidBCCEmailAddress")]
    [StringLength(80)]
    public string? BCC { get; set; }
}
