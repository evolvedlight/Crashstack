using System.ComponentModel.DataAnnotations.Schema;

namespace Crashstack.Data.Entities
{
    public class Issue
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Project Project { get; set; }

        public Guid ProjectId { get; set; }
        public DateTime CreatedAt { get; set; }
        public required string Title { get; set; }
        public string? Level { get; set; }
        public IssueStatus Status { get; set; }

        // Short term hack - find where the stacktrace matches
        public required string IssueSearchField { get; set; }

        public ICollection<CrashstackEvent> Events { get; set; } = [];
    }

    public enum IssueStatus
    {
        NotSet = 0,
        Open = 1,
        Resolved = 2,
        Ignored = 3
    }
}
