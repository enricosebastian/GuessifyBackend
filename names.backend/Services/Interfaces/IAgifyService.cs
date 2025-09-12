using Microsoft.AspNetCore.Mvc;
using names.backend.Models;

namespace names.backend.Services.Interfaces
{
    public interface IAgifyService
    {
        public Task<AgifyResponseDto> GetAgifyResult(string name, string countryId);
        public Task<List<AgifyResponseDto>> GetAgifyResult(List<string> names);
    }
}
