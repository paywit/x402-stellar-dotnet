using System.Text.Json.Serialization;

namespace X402.Stellar.Types;

public sealed class PaymentPayload
{
    [JsonPropertyName("x402Version")]
    public int X402Version { get; set; } = 2;

    [JsonPropertyName("resource")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ResourceInfo? Resource { get; set; }

    [JsonPropertyName("accepted")]
    public required PaymentRequirements Accepted { get; set; }

    [JsonPropertyName("payload")]
    public required Dictionary<string, object> Payload { get; set; }

    [JsonPropertyName("extensions")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<string, object>? Extensions { get; set; }
}
