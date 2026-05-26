using Microsoft.AspNetCore.Mvc;
using TestTaskRosa.Models;
using TestTaskRosa.Services;

namespace TestTaskRosa.Controllers
{
    [ApiController]
    [Route("api/requests")]
    public class RequestController : ControllerBase
    {
        private readonly RequestService _service;

        public RequestController(RequestService service)
        {
            _service = service;
        }


        [HttpPost]
        public async Task<IActionResult> Create(Request request)
        {
            try
            {
                return Ok(await _service.Create(request));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? employeeName)
        {
            if (!string.IsNullOrEmpty(employeeName))
            {
                return Ok(await _service.GetByEmployee(employeeName));
            }
            return Ok(await _service.GetAll());
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetById(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id, RequestStatus status)
        {
            try
            {
                await _service.UpdateStatus(id, status);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
