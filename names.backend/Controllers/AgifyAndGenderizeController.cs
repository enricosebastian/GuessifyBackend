using Microsoft.AspNetCore.Mvc;
using names.backend.Services.Interfaces;

namespace names.backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AgifyAndGenderizeController : Controller
    {
        private readonly IAgifyService _agifyService;
        private readonly IGenderizeService _genderizeService;
        private readonly IRestCountriesService _apiFirstCountriesService;

        public AgifyAndGenderizeController(IAgifyService agifyService, IGenderizeService genderizeService, IRestCountriesService apiFirstCountriesService)
        {
            _agifyService = agifyService;
            _genderizeService = genderizeService;
            _apiFirstCountriesService = apiFirstCountriesService;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get(string name, string countryId = "")
        {
            var agifyResponse = await _agifyService.GetAgifyResult(name, countryId);
            var genderizeResponse = await _genderizeService.GetGenderizeResult(name, countryId);

            if (!string.IsNullOrEmpty(countryId))
            {
                var data = await _apiFirstCountriesService.Get(countryId);

                return Ok(new
                {
                    name,
                    agifyResponse.Age,
                    genderizeResponse.Gender,
                    gender_probability = genderizeResponse.Probability,
                    country = data.Name.Official
                });

            }

            return Ok(new
            {
                name,
                agifyResponse.Age,
                genderizeResponse.Gender,
                gender_probability = genderizeResponse.Probability
            });
        }
    }
}
