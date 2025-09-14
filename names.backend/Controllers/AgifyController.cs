using Microsoft.AspNetCore.Mvc;
using names.backend.Services.Interfaces;

namespace names.backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AgifyController : Controller
    {
        private readonly IAgifyService _agifyService;
        public AgifyController(IAgifyService agifyService)
        {
            _agifyService = agifyService;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get(string name, string countryId = "")
        {
            try
            {
                var response = await _agifyService.GetAgifyResult(name, countryId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Agify/Get: {ex.Message}");
                return StatusCode(500, new { success = false, error = ex.Message });
            }
        }

        [HttpGet("GetMany")]
        public async Task<IActionResult> Get([FromQuery] List<string> name)
        {
            try
            {
                var response = await _agifyService.GetAgifyResult(name);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Agify/GetMany: {ex.Message}");
                return StatusCode(500, new { success = false, error = ex.Message });
            }
        }
    }
}
