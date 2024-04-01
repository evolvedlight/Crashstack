namespace Crashstack.Data.Entities;

public class CrashstackEvent
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime EventTimestamp { get; set; }
    public Issue Issue { get; set; }
    public string? Type { get; set; }
    public string? Value { get; set; }
    public string? Module { get; set; }
    public string? ThreadId { get; set; }
    public string? StackTrace { get; set; }
    public string? Level { get; set; }
}
