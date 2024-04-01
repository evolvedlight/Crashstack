using System.Text.Json.Serialization;

namespace SentryParser.Model;

public class SentryEvent
{
    public long? Length { get; set; }
    public Dictionary<string, List<SentryException>>? Exception { get; set; }

    public Dictionary<string, string>? Modules { get; set; }

    [JsonPropertyName("event_id")]
    public string? EventId { get; set; }

    public DateTime Timestamp { get; set; }

    [JsonPropertyName("logentry")]
    public LogEntry? LogEntry { get; set; }
    public string? Platform { get; set; }
    public string? Release { get; set; }

    [JsonPropertyName("server_name")]
    public string? ServerName { get; set; }
    public string? Logger { get; set; }
    public string? Level { get; set; }
    public string? Transaction { get; set; }
    public SentryRequest? Request { get; set; }
    public SentryUser? User { get; set; }
    public string? Environment { get; set; }
    public Dictionary<string, string>? Tags { get; set; }

}

public class SentryUser
{
    [JsonPropertyName("ip_address")]
    public string? IpAddress { get; set; }
}

public class SentryRequest
{
    public Dictionary<string, string>? Env { get; set; }
    public Dictionary<string, string>? Headers { get; set; }
    public string? Url { get; set; }
    public string? Method { get; set; }
    public string? Cookies { get; set; }
}

public class LogEntry
{
    public string? Message { get; set; }
    public string? Formatted { get; set; }
}