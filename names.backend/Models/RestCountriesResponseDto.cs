using System.Text.Json.Serialization;

namespace names.backend.Models
{
    public class RestCountriesResponseDto
    {
        [JsonPropertyName("name")]
        public CountryName Name { get; set; }

        [JsonPropertyName("cca2")]
        public string Iso2 { get; set; }
    }

    public class CountryName
    {
        [JsonPropertyName("common")]
        public string Common { get; set; }

        [JsonPropertyName("official")]
        public string Official { get; set; }
    }

}
