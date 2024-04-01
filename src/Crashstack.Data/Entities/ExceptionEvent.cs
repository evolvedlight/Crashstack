namespace Crashstack.Data.Entities;

public class ExceptionEvent
{
    public Guid Id { get; set; }
    public string? Type { get; set; }
    public string? Value { get; set; }
    public string? Module { get; set; }
    public string? ThreadId { get; set; }
    public string? StackTrace { get; set; }
    public string? Level { get; set; }
    public string? Transaction { get; set; }

}
