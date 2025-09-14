using Microsoft.Extensions.Options;
using names.backend.Models;
using names.backend.Services.Interfaces;
using System.Xml.Linq;

namespace names.backend.Services
{
    public class RestCountriesService : IRestCountriesService
    {
        private readonly IOptions<AppSettings> _appSettings;
        
        public RestCountriesService(IOptions<AppSettings> appSettings)
        {
            if (string.IsNullOrEmpty(appSettings.Value.RestCountriesBaseUrl))
            {
                throw new Exception("AppSettings value for ApiFirstCountriesService is empty");
            }

            _appSettings = appSettings;
        }

        /// <summary>
        /// Finds a specific country based on ISO-2 country code
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<RestCountriesResponseDto>> Find(string countryCode)
        {
            var requestUri = string.Empty;

            if (string.IsNullOrEmpty(countryCode))
            {
                throw new Exception("No countryCode value");
            }

            if (countryCode.Length != 2)
            {
                throw new Exception($"Country code \"{countryCode}\" is not ISO-2 format");
            }

            requestUri = $"{_appSettings.Value.RestCountriesBaseUrl}/alpha/{countryCode}";

            var httpClient = new HttpClient();
            var response = await httpClient.GetFromJsonAsync<List<RestCountriesResponseDto>>(requestUri);
            return response;
        }

        public async Task<RestCountriesResponseDto> Get(string countryCode)
        {
            if (string.IsNullOrEmpty(countryCode))
            {
                throw new Exception("No countryCode value");
            }

            if (countryCode.Length != 2)
            {
                throw new Exception($"Country code \"{countryCode}\" is not ISO-2 format");
            }

            var result = await Find(countryCode);
            var countryInfo = result.Where(r => r.Iso2 == countryCode.ToUpper()).FirstOrDefault();

            if (countryInfo == null)
            {
                throw new Exception($"Could not find country code \"{countryCode}\" in API call");
            }

            return countryInfo;
        }

        /// <summary>
        /// Gets all of the countries
        /// </summary>
        /// <returns></returns>
        public async Task<List<RestCountriesResponseDto>> GetAll()
        {
            var requestUri = $"{_appSettings.Value.RestCountriesBaseUrl}/all?fields=name,cca2&format=json";
            var httpClient = new HttpClient();
            var response = await httpClient.GetFromJsonAsync<List<RestCountriesResponseDto>>(requestUri);
            return response;
        }
    }
}
