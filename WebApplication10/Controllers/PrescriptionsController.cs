using Microsoft.AspNetCore.Mvc;
using WebApplication10.DTOs;
using WebApplication10.Services;

namespace WebApplication10.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrescriptionsController : ControllerBase
    {
        private readonly IPrescriptionService _service;

        public PrescriptionsController(IPrescriptionService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddPrescription([FromBody] CreatePrescriptionRequest request)
        {
            var result = await _service.AddPrescriptionAsync(request);

            if (result.Contains("not found") || result.Contains("maximum") || result.Contains("DueDate"))
                return BadRequest(result);

            return Ok(result);
        }
    }
}