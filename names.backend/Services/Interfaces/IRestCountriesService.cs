using names.backend.Models;

namespace names.backend.Services.Interfaces
{
    public interface IRestCountriesService
    {
        public Task<List<RestCountriesResponseDto>> Find(string countryCode);
        public Task<RestCountriesResponseDto> Get(string countryCode);
        public Task<List<RestCountriesResponseDto>> GetAll();

    }
}
