using Crashstack.Data;
using Crashstack.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SentryParser;
using SentryParser.Model;
using System.IO;
using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Crashstack.Server.Hubs;
using Crashstack.Contracts;

namespace Crashstack.Server.Controllers;

[ApiController]
public class EnvelopeController : ControllerBase
{
	private readonly ILogger<EnvelopeController> _logger;
	private readonly CrashstackDbContext _db;
    private readonly IHubContext<CrashstackHub> _hubContext;

    public EnvelopeController(ILogger<EnvelopeController> logger, CrashstackDbContext context, IHubContext<CrashstackHub> hubContext)
    {
        _logger = logger;
        _db = context;
        _hubContext = hubContext;
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
				var issueSearchField = exception.Type;
				if (exception.Stacktrace?.Frames?.Any() == true)
				{
					issueSearchField += exception.Stacktrace.Frames.Last().Function;
				}
				// Naive way - go by just one top stack frame
				var candidateIssues = await _db.Issues
					.Where(i => i.IssueSearchField == issueSearchField)
					.ToListAsync();

				Issue issue;
				if (candidateIssues.Any())
				{
					issue = candidateIssues.OrderByDescending(i => i.CreatedAt).First();
				}
				else
				{
					issue = new Issue
					{
						IssueSearchField = issueSearchField,
						Title = exception.Value,
						CreatedAt = DateTime.UtcNow,
						Level = sentryEvent.Level,
						Status = IssueStatus.Open,
					};
					_db.Issues.Add(issue);
                }

				var sentryTimeStampUtc = DateTime.SpecifyKind(sentryEvent.Timestamp, DateTimeKind.Utc);
				var csEvent = new CrashstackEvent
				{
					Type = exception.Type,
					Issue = issue,
					CreatedAt = DateTime.UtcNow,
					Level = sentryEvent.Level,
					Module = exception.Module,
					EventTimestamp = sentryTimeStampUtc
				};
				_db.CrashstackEvents.Add(csEvent);

                var updatedIssueDto = new IssueDto
                {
                    Id = issue.Id,
                    Title = issue.Title,
                    CreatedAt = issue.CreatedAt,
                    LastSeen = sentryTimeStampUtc,
                };

                await _hubContext.Clients.All.SendAsync("issueReceived", updatedIssueDto);
            }
		}
		await _db.SaveChangesAsync();
	}
}

