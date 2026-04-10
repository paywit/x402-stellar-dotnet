using System.Text.Json.Serialization;

namespace X402.Stellar.Types;

public sealed class SupportedKind
{
    [JsonPropertyName("x402Version")]
    public required int X402Version { get; set; }

    [JsonPropertyName("scheme")]
    public required string Scheme { get; set; }

    [JsonPropertyName("network")]
    public required string Network { get; set; }

    [JsonPropertyName("extra")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<string, object>? Extra { get; set; }
}

public sealed class SupportedResponse
{
    [JsonPropertyName("kinds")]
    public required SupportedKind[] Kinds { get; set; }

    [JsonPropertyName("extensions")]
    public required string[] Extensions { get; set; }

    [JsonPropertyName("signers")]
    public required Dictionary<string, string[]> Signers { get; set; }
}
