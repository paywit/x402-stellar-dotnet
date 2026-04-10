using X402.Stellar.Constants;
using X402.Stellar.Http;
using X402.Stellar.Types;

namespace X402.Stellar.Tests;

public class PaymentHeaderParserTests
{
    [Fact]
    public void Parse_WithV2Header_ReturnsPayload()
    {
        var payload = new PaymentPayload
        {
            X402Version = 2,
            Accepted = new PaymentRequirements
            {
                Scheme = "exact",
                Network = "stellar:testnet",
                Asset = "CBIELTK6YBZJU5UP2WWQEUCYKLPU6AUNZ2BQ4WWFEIE3USCIHMXQDAMA",
                Amount = "100000",
                PayTo = "GABCDEF",
                MaxTimeoutSeconds = 60
            },
            Payload = new Dictionary<string, object> { ["transaction"] = "AAAA" }
        };

        var encoded = X402HeaderCodec.EncodePaymentPayload(payload);
        var headers = new Dictionary<string, string>
        {
            [X402Constants.PaymentSignatureHeader] = encoded
        };

        var result = X402PaymentHeaderParser.Parse(headers);

        Assert.True(result.HasPayment);
        Assert.Equal(2, result.Version);
        Assert.NotNull(result.PaymentPayload);
        Assert.Equal("exact", result.PaymentPayload!.Accepted.Scheme);
    }

    [Fact]
    public void Parse_WithV1Header_FallsBack()
    {
        var payload = new PaymentPayload
        {
            X402Version = 1,
            Accepted = new PaymentRequirements
            {
                Scheme = "exact",
                Network = "stellar:testnet",
                Asset = "CBIELTK6YBZJU5UP2WWQEUCYKLPU6AUNZ2BQ4WWFEIE3USCIHMXQDAMA",
                Amount = "50000",
                PayTo = "GABCDEF",
                MaxTimeoutSeconds = 30
            },
            Payload = new Dictionary<string, object> { ["transaction"] = "BBBB" }
        };

        var encoded = X402HeaderCodec.EncodePaymentPayload(payload);
        var headers = new Dictionary<string, string>
        {
            [X402Constants.LegacyPaymentHeader] = encoded
        };

        var result = X402PaymentHeaderParser.Parse(headers);

        Assert.True(result.HasPayment);
        Assert.Equal(1, result.Version);
    }

    [Fact]
    public void Parse_WithNoHeaders_ReturnsFalse()
    {
        var headers = new Dictionary<string, string>();
        var result = X402PaymentHeaderParser.Parse(headers);

        Assert.False(result.HasPayment);
        Assert.Null(result.PaymentPayload);
    }
}
