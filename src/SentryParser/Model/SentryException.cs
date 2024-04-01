using System.Text.Json.Serialization;

namespace SentryParser.Model;

public class SentryException
{
    public string? Type { get; set; }
    public string? Value { get; set; }
    public string? Module { get; set; }

    [JsonPropertyName("thread_id")]
    public long? ThreadId { get; set; }
    public SentryStacktrace? Stacktrace { get; set; }
    public SentryMechanism? Mechanism { get; set; }
}

public class SentryMechanism
{
    public string? Type { get; set; }
    public bool? Handled { get; set; }

}

public class SentryStacktrace
{
    public List<SentryFrame>? Frames { get; set; }
}

public class SentryFrame
{
    public string? Function { get; set; }

    [JsonPropertyName("in_app")]
    public bool? InApp { get; set; }

    public string? Package { get; set; }
    [JsonPropertyName("instruction_addr")]
    public string? InstructionAddr { get; set; }

    [JsonPropertyName("addr_mode")]
    public string? AddrMode { get; set; }

    [JsonPropertyName("function_id")]
    public string? FunctionId { get; set; }
}