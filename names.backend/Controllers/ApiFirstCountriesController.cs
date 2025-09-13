using Microsoft.AspNetCore.Mvc;
using names.backend.Services;
using names.backend.Services.Interfaces;

namespace names.backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiFirstCountriesController : Controller
    {
        private readonly IApiFirstCountriesService _apiFirstCountriesService;
        public ApiFirstCountriesController(IApiFirstCountriesService apiFirstCountriesService)
        {
            _apiFirstCountriesService = apiFirstCountriesService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            var response = await _apiFirstCountriesService.GetAll();
            return Ok(response.Data);
        }
    }
}
