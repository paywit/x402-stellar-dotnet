using X402.Stellar.Constants;
using X402.Stellar.Types;

namespace X402.Stellar.Http;

public sealed class PaymentHeaderResult
{
    public bool HasPayment { get; init; }
    public PaymentPayload? PaymentPayload { get; init; }
    public int Version { get; init; }
    public string? RawHeader { get; init; }
}

public static class X402PaymentHeaderParser
{
    public static PaymentHeaderResult Parse(IDictionary<string, string> headers)
    {
        // Try v2 header first
        if (headers.TryGetValue(X402Constants.PaymentSignatureHeader, out var v2Header)
            && !string.IsNullOrWhiteSpace(v2Header))
        {
            if (X402HeaderCodec.TryDecode<PaymentPayload>(v2Header, out var payload))
            {
                return new PaymentHeaderResult
                {
                    HasPayment = true,
                    PaymentPayload = payload,
                    Version = 2,
                    RawHeader = v2Header
                };
            }
        }

        // Fall back to v1 header
        if (headers.TryGetValue(X402Constants.LegacyPaymentHeader, out var v1Header)
            && !string.IsNullOrWhiteSpace(v1Header))
        {
            if (X402HeaderCodec.TryDecode<PaymentPayload>(v1Header, out var payload))
            {
                return new PaymentHeaderResult
                {
                    HasPayment = true,
                    PaymentPayload = payload,
                    Version = 1,
                    RawHeader = v1Header
                };
            }
        }

        return new PaymentHeaderResult { HasPayment = false };
    }

    public static PaymentHeaderResult ParseFromHttpHeaders(
        IEnumerable<KeyValuePair<string, IEnumerable<string>>> headers)
    {
        var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (var header in headers)
        {
            dict[header.Key] = header.Value.FirstOrDefault() ?? string.Empty;
        }
        return Parse(dict);
    }
}
