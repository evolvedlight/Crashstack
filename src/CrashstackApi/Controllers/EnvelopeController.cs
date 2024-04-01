using Crashstack.Data;
using Crashstack.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using SentryParser;
using SentryParser.Model;
using System.Text;

namespace CrashstackApi.Controllers
{
    [ApiController]
    public class EnvelopeController : ControllerBase
    {
        private readonly ILogger<EnvelopeController> _logger;
        private readonly CrashstackDbContext _db;

        public EnvelopeController(ILogger<EnvelopeController> logger, CrashstackDbContext context)
        {
            _logger = logger;
            _db = context;
        }

        [HttpPost("api/{projectId}/envelope")]
        public async Task<IActionResult> PostEnvelope(string projectId)
        {
            var bodyStream = Request.BodyReader.AsStream();
            var ms = new MemoryStream();
            bodyStream.CopyTo(ms);
            var envelope = Encoding.UTF8.GetString(ms.ToArray());

            var parsedEnvelopeItems = EnvelopeParser.Parse(envelope);

            foreach (var envelopeItem in parsedEnvelopeItems)
            {
                if (envelopeItem.Type is "event")
                {
                    if (envelopeItem?.Event?.Exception is not null)
                    {
                        await SaveError(envelopeItem.Event);
                    }
                }

                _logger.LogInformation("Received event type {type}", envelopeItem?.Type);
            }

            return Ok();
        }

        private async Task SaveError(SentryEvent sentryEvent)
        {
            if (sentryEvent?.Exception is null)
            {
                _logger.LogWarning("Not an exception");
                return;
            }

            foreach (var exceptionMap in sentryEvent.Exception)
            {
                foreach (var exception in exceptionMap.Value)
                {
                    var csEvent = new CrashstackEvent
                    {
                        Type = exception.Type
                    };
                    _db.CrashstackEvents.Add(csEvent);
                }
            }
            await _db.SaveChangesAsync();
        }
    }
}
