namespace SentryParser.Model;

public class SentryEnvelopeItem(string type, long length)
{
    public string Type { get; set; } = type;
    public long Length { get; set; } = length;
    public SentryAttachment? Attachment { get; set; }
    public SentryEvent? Event { get; set; }
}
