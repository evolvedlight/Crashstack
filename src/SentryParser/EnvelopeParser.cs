using SentryParser.Model;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace SentryParser;

public static class EnvelopeParser
{
    public static IEnumerable<SentryEvent> Parse(string input)
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
            //var withoutNewLine = part.Substring(0, part.LastIndexOf("\\n"));
            var json = JsonSerializer.Deserialize<JsonElement>(part);

            if (json.TryGetProperty("type", out var eventType))
            {
                

                var content = parts[eventId + 1];
                long? contentLength;
                if (json.TryGetProperty("length", out var lengthJsonNode))
                {
                    // Later we can do this well
                    contentLength = lengthJsonNode.GetInt64();
                }
                else
                {
                    contentLength = content.LongCount();
                }

                yield return new SentryEvent
                {
                    Length = contentLength,
                    Content = content,
                    Type = eventType.GetString(),
                };
            } 
            else
            {
                throw new InvalidDataException($"JSON event is missing type property");
            }
        }
    }
}
