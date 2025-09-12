using names.backend.Models;

namespace names.backend.Services.Interfaces
{
    public interface IApiFirstCountriesService
    {
        public Task<ApiFirstCountriesResponseDto> Find(string countryCode);
        public Task<CountryInfo> Get(string countryCode);
        public Task<ApiFirstCountriesResponseDto> GetAll();

    }
}
