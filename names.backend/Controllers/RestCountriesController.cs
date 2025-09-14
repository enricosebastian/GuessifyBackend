using Microsoft.AspNetCore.Mvc;
using names.backend.Services;
using names.backend.Services.Interfaces;

namespace names.backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestCountriesController : Controller
    {
        private readonly IRestCountriesService _apiFirstCountriesService;
        public RestCountriesController(IRestCountriesService apiFirstCountriesService)
        {
            _apiFirstCountriesService = apiFirstCountriesService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = (await _apiFirstCountriesService.GetAll()).OrderBy(r => r.Name.Official).ToList();
            return Ok(response);
        }
    }
}
