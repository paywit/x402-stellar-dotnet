using System.Text.Json.Serialization;

namespace X402.Stellar.Types;

public sealed class SettleRequest
{
    [JsonPropertyName("x402Version")]
    public int X402Version { get; set; } = 2;

    [JsonPropertyName("paymentPayload")]
    public required PaymentPayload PaymentPayload { get; set; }

    [JsonPropertyName("paymentRequirements")]
    public required PaymentRequirements PaymentRequirements { get; set; }
}
