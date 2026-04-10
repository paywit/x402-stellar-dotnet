using System.Text.Json.Serialization;

namespace X402.Stellar.Types;

public sealed class PaymentRequired
{
    [JsonPropertyName("x402Version")]
    public int X402Version { get; set; } = 2;

    [JsonPropertyName("error")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Error { get; set; }

    [JsonPropertyName("resource")]
    public required ResourceInfo Resource { get; set; }

    [JsonPropertyName("accepts")]
    public required PaymentRequirements[] Accepts { get; set; }

    [JsonPropertyName("extensions")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<string, object>? Extensions { get; set; }
}
