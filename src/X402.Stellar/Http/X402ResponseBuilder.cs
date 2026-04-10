using X402.Stellar.Constants;
using X402.Stellar.Types;

namespace X402.Stellar.Http;

public static class X402ResponseBuilder
{
    public static PaymentRequired BuildPaymentRequired(
        string resourceUrl,
        PaymentRequirements[] accepts,
        string? description = null,
        string? mimeType = null,
        string? error = null)
    {
        return new PaymentRequired
        {
            X402Version = X402Constants.ProtocolVersion,
            Error = error ?? "X-PAYMENT header is required",
            Resource = new ResourceInfo
            {
                Url = resourceUrl,
                Description = description,
                MimeType = mimeType
            },
            Accepts = accepts
        };
    }

    public static PaymentRequirements BuildStellarRequirements(
        string payTo,
        string amount,
        string network = "stellar:testnet",
        string? asset = null,
        int maxTimeoutSeconds = 60)
    {
        asset ??= network == StellarConstants.PubnetNetwork
            ? StellarConstants.UsdcMainnet
            : StellarConstants.UsdcTestnet;

        return new PaymentRequirements
        {
            Scheme = X402Constants.ExactScheme,
            Network = network,
            Asset = asset,
            Amount = amount,
            PayTo = payTo,
            MaxTimeoutSeconds = maxTimeoutSeconds,
            Extra = new Dictionary<string, object>
            {
                ["areFeesSponsored"] = true
            }
        };
    }

    public static string AmountFromDecimal(decimal price, int decimals = StellarConstants.DefaultTokenDecimals)
    {
        var multiplier = (decimal)Math.Pow(10, decimals);
        var atomicUnits = Math.Truncate(price * multiplier);
        return atomicUnits.ToString("F0");
    }
}
