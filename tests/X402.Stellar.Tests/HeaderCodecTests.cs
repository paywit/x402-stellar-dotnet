using X402.Stellar.Http;
using X402.Stellar.Types;

namespace X402.Stellar.Tests;

public class HeaderCodecTests
{
    [Fact]
    public void Encode_Decode_PaymentRequired_RoundTrips()
    {
        var original = new PaymentRequired
        {
            X402Version = 2,
            Error = "Payment required",
            Resource = new ResourceInfo
            {
                Url = "https://api.example.com/weather",
                Description = "Weather API",
                MimeType = "application/json"
            },
            Accepts = new[]
            {
                new PaymentRequirements
                {
                    Scheme = "exact",
                    Network = "stellar:testnet",
                    Asset = "CBIELTK6YBZJU5UP2WWQEUCYKLPU6AUNZ2BQ4WWFEIE3USCIHMXQDAMA",
                    Amount = "100000",
                    PayTo = "GABCDEFGHIJKLMNOPQRSTUVWXYZ234567ABCDEFGHIJKLMNOPQRSTUV",
                    MaxTimeoutSeconds = 60,
                    Extra = new Dictionary<string, object> { ["areFeesSponsored"] = true }
                }
            }
        };

        var encoded = X402HeaderCodec.EncodePaymentRequired(original);
        var decoded = X402HeaderCodec.DecodePaymentRequired(encoded);

        Assert.Equal(original.X402Version, decoded.X402Version);
        Assert.Equal(original.Error, decoded.Error);
        Assert.Equal(original.Resource.Url, decoded.Resource.Url);
        Assert.Equal(original.Resource.Description, decoded.Resource.Description);
        Assert.Single(decoded.Accepts);
        Assert.Equal("exact", decoded.Accepts[0].Scheme);
        Assert.Equal("stellar:testnet", decoded.Accepts[0].Network);
        Assert.Equal("100000", decoded.Accepts[0].Amount);
    }

    [Fact]
    public void Encode_Decode_PaymentPayload_RoundTrips()
    {
        var original = new PaymentPayload
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
            Payload = new Dictionary<string, object>
            {
                ["transaction"] = "AAAA..."
            }
        };

        var encoded = X402HeaderCodec.EncodePaymentPayload(original);
        var decoded = X402HeaderCodec.DecodePaymentPayload(encoded);

        Assert.Equal(2, decoded.X402Version);
        Assert.Equal("exact", decoded.Accepted.Scheme);
        Assert.Equal("stellar:testnet", decoded.Accepted.Network);
    }

    [Fact]
    public void TryDecode_InvalidBase64_ReturnsFalse()
    {
        var result = X402HeaderCodec.TryDecode<PaymentRequired>("not-valid-base64!!!", out var decoded);
        Assert.False(result);
        Assert.Null(decoded);
    }

    [Fact]
    public void Encode_ProducesValidBase64()
    {
        var resource = new ResourceInfo { Url = "https://example.com" };
        var encoded = X402HeaderCodec.Encode(resource);

        var bytes = Convert.FromBase64String(encoded);
        Assert.NotEmpty(bytes);
    }
}
