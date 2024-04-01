using Microsoft.AspNetCore.Mvc;
using SentryParser;
using System.Text;
using System.Text.Json.Nodes;

namespace CrashstackApi.Controllers
{
    [ApiController]
    public class EnvelopeController : ControllerBase
    {
        private readonly ILogger<EnvelopeController> _logger;

        public EnvelopeController(ILogger<EnvelopeController> logger)
        {
            _logger = logger;
        }

        [HttpPost("api/{projectId}/envelope")]
        public async Task<IActionResult> PostEnvelope(string projectId)
        {
            var bodyStream = Request.BodyReader.AsStream();
            var ms = new MemoryStream();
            bodyStream.CopyTo(ms);
            var envelope = Encoding.UTF8.GetString(ms.ToArray());

            var parsedEvents = EnvelopeParser.Parse(envelope);

            //_logger.LogInformation("Recieved event {Events}", parsedEvents);

            foreach (var thing in parsedEvents)
            {
                _logger.LogInformation("Recieved event {Events}", thing.Content);
            }

            return Ok();
        }
    }

    public class Envelope
    {
        public List<JsonNode> Events { get; set; }
    }

    public class Event
    {
    }
}
