namespace Crashstack.Contracts;

public class IssueDto
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Title { get; set; }
    public DateTime? LastSeen { get; set; }
}
