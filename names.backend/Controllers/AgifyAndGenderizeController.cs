using Microsoft.AspNetCore.Mvc;
using names.backend.Models;
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
            AgifyResponseDto? agifyResponse = null;
            GenderizeResponseDto? genderizeResponse = null;

            try
            {
                agifyResponse = await _agifyService.GetAgifyResult(name, countryId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AgifyAndGenderize/Get for Agify: {ex.Message}");
                return StatusCode(500, new { success = false, error = ex.Message });
            }

            try
            {
                genderizeResponse = await _genderizeService.GetGenderizeResult(name, countryId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AgifyAndGenderize/Get for Genderize: {ex.Message}");
                return StatusCode(500, new { success = false, error = ex.Message });
            }

            if (string.IsNullOrEmpty(countryId))
            {
                return Ok(new
                {
                    name,
                    agifyResponse.Age,
                    genderizeResponse.Gender,
                    gender_probability = genderizeResponse.Probability
                });
            }


            RestCountriesResponseDto? restCountriesResponse = null;

            try
            {
                restCountriesResponse = await _apiFirstCountriesService.Get(countryId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AgifyAndGenderize/Get for RestCountries: {ex.Message}");
                return StatusCode(500, new { success = false, error = ex.Message });
            }

            return Ok(new
            {
                name,
                agifyResponse.Age,
                genderizeResponse.Gender,
                gender_probability = genderizeResponse.Probability,
                country = restCountriesResponse.Name.Common,
                fancy_name = restCountriesResponse.Name.Official
            });
        }
    }
}
