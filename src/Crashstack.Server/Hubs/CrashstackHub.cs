using Crashstack.Contracts;
using Crashstack.Data;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Crashstack.Server.Hubs
{
    public class CrashstackHub : Hub
    {
        private readonly ILogger<CrashstackHub> _logger;
        private readonly CrashstackDbContext _db;

        public CrashstackHub(ILogger<CrashstackHub> logger, CrashstackDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public override async Task OnConnectedAsync()
        {
            _logger.LogInformation("Client connected. Sending all projects");
            await Clients.Caller.SendAsync("projectReceived", new AddProjectMessage(Guid.NewGuid().ToString(), "Default"));
            await base.OnConnectedAsync();
        }

        public async Task SubscribeToProject(string projectId)
        {
            _logger.LogInformation("Client subscribed to project {projectId}", projectId);
            await Groups.AddToGroupAsync(Context.ConnectionId, projectId);

            var currentIssues = await _db.Issues
                .Select(e => new IssueDto
                {
                    Id = e.Id,
                    CreatedAt = e.CreatedAt,
                    Title = e.Title,
                    LastSeen = e.Events.OrderByDescending(e => e.EventTimestamp).FirstOrDefault().EventTimestamp,   
                })
                .ToListAsync();

            _logger.LogInformation("Sending {count} issues to client", currentIssues.Count);

            foreach (var issue in currentIssues)
            {
                await Clients.Caller.SendAsync("issueReceived", issue);
            }
        }
    }

    public record AddProjectMessage (string Id, string projectName);
}
