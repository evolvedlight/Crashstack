using System;
using System.Collections.Generic;

namespace SentryParser.Model;

public class SentryEvent
{
    public long? Length { get; internal set; }
    public string Content { get; internal set; }
    public string? Type { get; internal set; }
}
