using SentryParser.Model;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace SentryParser;

public static class EnvelopeParser
{
    public static IEnumerable<SentryEnvelopeItem> Parse(string input)
    {
        var parts = input.Split("\n", StringSplitOptions.TrimEntries);

        for (int eventId = 1; eventId < parts.Length; eventId += 2)
        {
            string? part = parts[eventId];

            // the last can be empty
            if (eventId == parts.Length - 1 && string.IsNullOrEmpty(part))
            {
                continue;
            }
            var json = JsonSerializer.Deserialize<JsonElement>(part);

            if (!json.TryGetProperty("type", out var eventTypeNode))
            {
                throw new InvalidDataException($"JSON event is missing type property");
            }

            var content = parts[eventId + 1];
            long contentLength;
            if (json.TryGetProperty("length", out var lengthJsonNode))
            {
                // Later we can do this well
                contentLength = lengthJsonNode.GetInt64();
            }
            else
            {
                contentLength = content.LongCount();
            }

            var eventType = eventTypeNode.GetString() ?? throw new InvalidDataException($"JSON event is missing type property");

            var result = new SentryEnvelopeItem(eventType, contentLength);

            switch (eventType)
            {
                //case "transaction":
                //    var transaction = JsonSerializer.Deserialize<SentryTransaction>(content);
                //    break;
                case "attachment":
                    result.Attachment = new SentryAttachment(content);
                    yield return result;
                    break;
                //case "session":
                //    var session = JsonSerializer.Deserialize<SentrySession>(content);
                //    break;
                case "event":
                    result.Event = JsonSerializer.Deserialize<SentryEvent>(content, JsonDefaults.JSON_DEFAULTS);
                    yield return result;
                    break;
                default:
                    yield return result;
                    break;
            }
        }
    }
}
