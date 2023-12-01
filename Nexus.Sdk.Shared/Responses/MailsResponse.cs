using System.Text.Json.Serialization;

namespace Nexus.Sdk.Shared.Responses
{
    public record MailsResponse
    {
        [JsonConstructor]
        public MailsResponse(string code, string created, string sent, string status, string type, int? count, MailEntityCodes references, MailContent content, MailRecipient recipient)
        {
            Code = code;
            Created = created;
            Sent = sent;
            Status = status;
            Type = type;
            Count = count;
            References = references;
            Content = content;
            Recipient = recipient;
        }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("created")]
        public string Created { get; set; }

        [JsonPropertyName("sent")]
        public string Sent { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("count")]
        public int? Count { get; set; }

        [JsonPropertyName("references")]
        public MailEntityCodes References { get; set; }

        [JsonPropertyName("content")]
        public MailContent Content { get; set; }

        [JsonPropertyName("recipient")]
        public MailRecipient Recipient { get; set; }
    }

    public class MailEntityCodes
    {
        [JsonConstructor]
        public MailEntityCodes(string accountCode, string customerCode, string tokenPaymentCode)
        {
            AccountCode = accountCode;
            CustomerCode = customerCode;
            TokenPaymentCode = tokenPaymentCode;
        }

        [JsonPropertyName("accountCode")]
        public string AccountCode { get; set; }

        [JsonPropertyName("customerCode")]
        public string CustomerCode { get; set; }

        [JsonPropertyName("tokenPaymentCode")]
        public string TokenPaymentCode { get; set; }
    }

    public class MailContent
    {
        [JsonConstructor]
        public MailContent(string subject, string html, string text)
        {
            Subject = subject;
            Html = html;
            Text = text;
        }

        [JsonPropertyName("subject")]
        public string Subject { get; set; }

        [JsonPropertyName("html")]
        public string Html { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }
    }

    public class MailRecipient
    {
        [JsonConstructor]
        public MailRecipient(string email, string cC, string bCC)
        {
            Email = email;
            CC = cC;
            BCC = bCC;
        }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("cc")]
        public string CC { get; set; }

        [JsonPropertyName("bcc")]
        public string BCC { get; set; }
    }
}
