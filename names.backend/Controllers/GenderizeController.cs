using Microsoft.AspNetCore.Mvc;
using names.backend.Services;
using names.backend.Services.Interfaces;

namespace names.backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenderizeController : Controller
    {
        private readonly IGenderizeService _genderizeService;
        public GenderizeController(IGenderizeService genderizeService)
        {
            _genderizeService = genderizeService;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get(string name, string countryId = "")
        {
            try
            {
                var response = await _genderizeService.GetGenderizeResult(name, countryId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Genderize/Get: {ex.Message}");
                return StatusCode(500, new { success = false, error = ex.Message });
            }
        }

        [HttpGet("GetMany")]
        public async Task<IActionResult> Get([FromQuery] List<string> name)
        {
            try
            {
                var response = await _genderizeService.GetGenderizeResult(name);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Genderize/GetMany: {ex.Message}");
                return StatusCode(500, new { success = false, error = ex.Message });
            }
        }
    }
}
