using ConsumerAPI.BackgroundServices;
using ConsumerAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Models;

namespace ConsumerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepairRequestsController : ControllerBase
    {
        private readonly DatabaseService _databaseService;
        private readonly MessageListenerService _messageListenerService;

        public RepairRequestsController(DatabaseService databaseService, MessageListenerService messageListenerService)
        {
            _databaseService = databaseService;
            _messageListenerService = messageListenerService;
        }

        [HttpPost("consume")]
        public IActionResult ConsumeMessage()
        {
            return Ok("Started Consuming messages from the queue.");
        }

        //View all processed repair requests
        [HttpGet("Processed-Requests")]
        public async Task<ActionResult<IEnumerable<RepairRequest>>> GetProcessedRequests()
        {
            var requests = await _databaseService.GetProcessedRequestsAsync();
            return Ok(requests);
        }
    }
}
