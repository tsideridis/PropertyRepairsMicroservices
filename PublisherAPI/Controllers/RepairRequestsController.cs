using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublisherAPI.Services;

namespace PublisherAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepairRequestsController : ControllerBase
    {
        private readonly QueueService _queueService;
        public RepairRequestsController(QueueService queueService)
        {
            _queueService = queueService;
        }

        [HttpPost]
        public IActionResult CreateRepairRequest([FromBody] SharedLibrary.Models.RepairRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _queueService.PublishMessage(request);
                return Ok($"Repair Request for {request.FullName} added to the queue");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
