using System.Text.Json.Serialization;

namespace X402.Stellar.Types;

public sealed class VerifyResponse
{
    [JsonPropertyName("isValid")]
    public required bool IsValid { get; set; }

    [JsonPropertyName("invalidReason")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? InvalidReason { get; set; }

    [JsonPropertyName("invalidMessage")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? InvalidMessage { get; set; }

    [JsonPropertyName("payer")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Payer { get; set; }

    [JsonPropertyName("extensions")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<string, object>? Extensions { get; set; }
}
