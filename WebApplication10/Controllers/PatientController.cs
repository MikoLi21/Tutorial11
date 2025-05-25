using Microsoft.AspNetCore.Mvc;
using WebApplication10.Services;

namespace WebApplication10.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _service;

        public PatientController(IPatientService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatient(int id)
        {
            var result = await _service.GetPatientAsync(id);

            if (result == null)
                return NotFound($"Patient with ID {id} not found.");

            return Ok(result);
        }
    }
}