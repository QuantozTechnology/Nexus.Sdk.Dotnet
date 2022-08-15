using System.Text.Json.Serialization;

namespace Nexus.SDK.Shared.Responses
{
    public record PagedResponse<T>
    {
        [JsonConstructor]
        public PagedResponse(int page, int total, int totalPages,
            IDictionary<string, string> filteringParameters,
            IEnumerable<T> records)
        {
            Page = page;
            Total = total;
            TotalPages = totalPages;
            FilteringParameters = filteringParameters;
            Records = records;
        }

        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }

        [JsonPropertyName("totalPages")]
        public int TotalPages { get; set; }

        [JsonPropertyName("filteringParameters")]
        public IDictionary<string, string> FilteringParameters { get; set; }

        [JsonPropertyName("records")]
        public IEnumerable<T> Records { get; set; }
    }
}
