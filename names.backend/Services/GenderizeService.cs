using Microsoft.Extensions.Options;
using names.backend.Models;
using names.backend.Services.Interfaces;

namespace names.backend.Services
{
    public class GenderizeService : IGenderizeService
    {
        private readonly IOptions<AppSettings> _appSettings;

        public GenderizeService(IOptions<AppSettings> appSettings)
        {
            if (string.IsNullOrEmpty(appSettings.Value.GenderizeBaseUrl))
            {
                Console.WriteLine("AppSettings value for Genderize is empty");
                throw new Exception("AppSettings value for Genderize is empty");
            }

            _appSettings = appSettings;
        }

        public async Task<GenderizeResponseDto> GetGenderizeResult(string name, string countryId)
        {
            var requestUri = string.Empty;

            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("No name value");
                throw new Exception("No name value");
            }

            if (string.IsNullOrEmpty(countryId))
            {
                requestUri = $"{_appSettings.Value.GenderizeBaseUrl}?name={name}";
            }

            if (!string.IsNullOrEmpty(countryId))
            {
                requestUri = $"{_appSettings.Value.GenderizeBaseUrl}?name={name}&country_id={countryId}";
            }

            var httpClient = new HttpClient();
            GenderizeResponseDto? response = null;

            try
            {
                response = await httpClient.GetFromJsonAsync<GenderizeResponseDto>(requestUri);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching genderize data: {ex.Message}");

                requestUri = $"{_appSettings.Value.GenderizeBaseUrl}?name={name}";
                response = await httpClient.GetFromJsonAsync<GenderizeResponseDto>(requestUri);
            }
            
            return response;
        }

        public async Task<List<GenderizeResponseDto>> GetGenderizeResult(List<string> names)
        {
            var requestUri = string.Empty;

            if (names.Count < 1)
            {
                Console.WriteLine("Emtpy list of names");
                throw new Exception("Emtpy list of names");
            }

            requestUri = $"{_appSettings.Value.GenderizeBaseUrl}?";

            foreach (var name in names)
            {
                requestUri += $"name[]={name}&";
            }

            var httpClient = new HttpClient();
            var response = await httpClient.GetFromJsonAsync<List<GenderizeResponseDto>>(requestUri);
            return response;
        }
    }
}
