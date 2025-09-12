using System.Text.Json.Serialization;

namespace names.backend.Models
{
    /// <summary>
    /// Modeled after Agify response data: https://agify.io/documentation
    /// </summary>
    public class AgifyResponseDto
    {
        [JsonPropertyName("count")]
        public int Count { get; set; } = 0;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("age")]
        public int Age { get; set; } = 0;

        [JsonPropertyName("country_id")]
        public string CountryId { get; set; } = string.Empty;
    }
}