namespace X402.Stellar.Constants;

public static class ErrorCodes
{
    public const string InsufficientFunds = "insufficient_funds";
    public const string InvalidNetwork = "invalid_network";
    public const string InvalidPayload = "invalid_payload";
    public const string InvalidPaymentRequirements = "invalid_payment_requirements";
    public const string InvalidScheme = "invalid_scheme";
    public const string UnsupportedScheme = "unsupported_scheme";
    public const string InvalidX402Version = "invalid_x402_version";
    public const string InvalidTransactionState = "invalid_transaction_state";
    public const string UnexpectedVerifyError = "unexpected_verify_error";
    public const string UnexpectedSettleError = "unexpected_settle_error";
    public const string NetworkMismatch = "network_mismatch";
    public const string InvalidStellarPayloadMalformed = "invalid_exact_stellar_payload_malformed";
}
