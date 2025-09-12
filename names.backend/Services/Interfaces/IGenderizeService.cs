using names.backend.Models;

namespace names.backend.Services.Interfaces
{
    public interface IGenderizeService
    {
        public Task<GenderizeResponseDto> GetGenderizeResult(string name, string countryId);
        public Task<List<GenderizeResponseDto>> GetGenderizeResult(List<string> names);
    }
}
