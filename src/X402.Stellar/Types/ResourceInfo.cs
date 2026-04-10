using System.Text.Json.Serialization;

namespace X402.Stellar.Types;

public sealed class ResourceInfo
{
    [JsonPropertyName("url")]
    public required string Url { get; set; }

    [JsonPropertyName("description")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Description { get; set; }

    [JsonPropertyName("mimeType")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? MimeType { get; set; }
}
