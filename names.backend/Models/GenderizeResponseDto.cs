using System.Text.Json.Serialization;

namespace names.backend.Models
{
    public class GenderizeResponseDto
    {
        [JsonPropertyName("count")]
        public int Count { get; set; } = 0;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("gender")]
        public string Gender { get; set; } = string.Empty;

        [JsonPropertyName("probability")]
        public decimal Probability { get; set; } = 0;

        [JsonPropertyName("country_id")]
        public string CountryId { get; set; } = string.Empty;
    }

    /*
     * 
        {
            "count": 1094417,
            "name": "peter",
            "gender": "male",
            "probability": 1
        }
     * 
     * 
     */
}
