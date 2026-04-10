namespace X402.Stellar.Constants;

public static class X402Constants
{
    public const int ProtocolVersion = 2;
    public const string ExactScheme = "exact";

    public const string DefaultFacilitatorUrl = "https://x402.org/facilitator";
    public const string OzTestnetFacilitatorUrl = "https://channels.openzeppelin.com/x402/testnet";
    public const string OzMainnetFacilitatorUrl = "https://channels.openzeppelin.com/x402";

    public const string PaymentRequiredHeader = "PAYMENT-REQUIRED";
    public const string PaymentSignatureHeader = "PAYMENT-SIGNATURE";
    public const string PaymentResponseHeader = "PAYMENT-RESPONSE";

    public const string LegacyPaymentHeader = "X-PAYMENT";
    public const string LegacyPaymentResponseHeader = "X-PAYMENT-RESPONSE";
}
