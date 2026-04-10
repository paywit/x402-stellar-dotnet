using System.Text.Json.Serialization;

namespace X402.Stellar.Types;

public sealed class DiscoveryResource
{
    [JsonPropertyName("resource")]
    public required string Resource { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; } = "http";

    [JsonPropertyName("x402Version")]
    public int X402Version { get; set; } = 2;

    [JsonPropertyName("accepts")]
    public required PaymentRequirements[] Accepts { get; set; }

    [JsonPropertyName("metadata")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<string, string>? Metadata { get; set; }
}

public sealed class DiscoveryResponse
{
    [JsonPropertyName("x402Version")]
    public int X402Version { get; set; } = 2;

    [JsonPropertyName("items")]
    public required DiscoveryResource[] Items { get; set; }
}
