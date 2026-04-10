using System.Text.Json.Serialization;

namespace X402.Stellar.Types;

public sealed class PaymentRequirements
{
    [JsonPropertyName("scheme")]
    public required string Scheme { get; set; }

    [JsonPropertyName("network")]
    public required string Network { get; set; }

    [JsonPropertyName("asset")]
    public required string Asset { get; set; }

    [JsonPropertyName("amount")]
    public required string Amount { get; set; }

    [JsonPropertyName("payTo")]
    public required string PayTo { get; set; }

    [JsonPropertyName("maxTimeoutSeconds")]
    public required int MaxTimeoutSeconds { get; set; }

    [JsonPropertyName("extra")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<string, object>? Extra { get; set; }
}
