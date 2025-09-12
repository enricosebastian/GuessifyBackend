using Microsoft.Extensions.Options;
using names.backend.Models;
using names.backend.Services.Interfaces;
using System.Xml.Linq;

namespace names.backend.Services
{
    public class AgifyService : IAgifyService
    {
        private readonly IOptions<AppSettings> _appSettings;

        public AgifyService(IOptions<AppSettings> appSettings)
        {
            if (string.IsNullOrEmpty(appSettings.Value.AgifyBaseUrl))
            {
                throw new Exception("AppSettings value for Agify is empty");
            }

            _appSettings = appSettings;
        }

        public async Task<AgifyResponseDto> GetAgifyResult(string name, string countryId)
        {
            var requestUri = string.Empty;

            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("No name value");
            }

            if (string.IsNullOrEmpty(countryId))
            {
                requestUri = $"{_appSettings.Value.AgifyBaseUrl}?name={name}";
            }

            if (!string.IsNullOrEmpty(countryId))
            {
                requestUri = $"{_appSettings.Value.AgifyBaseUrl}?name={name}&country_id={countryId}";
            }

            var httpClient = new HttpClient();
            var response = await httpClient.GetFromJsonAsync<AgifyResponseDto>(requestUri);
            return response;
        }

        public async Task<List<AgifyResponseDto>> GetAgifyResult(List<string> names)
        {
            var requestUri = string.Empty;

            if (names.Count < 1)
            {
                throw new Exception("Emtpy list of names");
            }

            requestUri = $"{_appSettings.Value.AgifyBaseUrl}?";

            foreach (var name in names)
            {
                requestUri += $"name[]={name}&";
            }

            var httpClient = new HttpClient();
            var response = await httpClient.GetFromJsonAsync<List<AgifyResponseDto>>(requestUri);
            return response;
        }
    }
}
