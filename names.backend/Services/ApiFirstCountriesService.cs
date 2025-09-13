using Microsoft.Extensions.Options;
using names.backend.Models;
using names.backend.Services.Interfaces;
using System.Xml.Linq;

namespace names.backend.Services
{
    public class ApiFirstCountriesService : IApiFirstCountriesService
    {
        private readonly IOptions<AppSettings> _appSettings;
        
        public ApiFirstCountriesService(IOptions<AppSettings> appSettings)
        {
            if (string.IsNullOrEmpty(appSettings.Value.ApiFirstCountriesBaseUrl))
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
        public async Task<ApiFirstCountriesResponseDto> Find(string countryCode)
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

            requestUri = $"{_appSettings.Value.ApiFirstCountriesBaseUrl}/countries?q={countryCode}";

            var httpClient = new HttpClient();
            var response = await httpClient.GetFromJsonAsync<ApiFirstCountriesResponseDto>(requestUri);
            return response;
        }

        public async Task<CountryInfo> Get(string countryCode)
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
            var countryInfo = result.Data.Where(d => d.Key == countryCode.ToUpper()).FirstOrDefault().Value;

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
        public async Task<ApiFirstCountriesResponseDto> GetAll()
        {
            var requestUri = $"{_appSettings.Value.ApiFirstCountriesBaseUrl}/countries";
            var httpClient = new HttpClient();
            var response = await httpClient.GetFromJsonAsync<ApiFirstCountriesResponseDto>(requestUri);
            return response;
        }
    }
}
