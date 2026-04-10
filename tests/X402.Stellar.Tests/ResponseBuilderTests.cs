using X402.Stellar.Constants;
using X402.Stellar.Http;

namespace X402.Stellar.Tests;

public class ResponseBuilderTests
{
    [Fact]
    public void BuildStellarRequirements_DefaultsToTestnetUsdc()
    {
        var requirements = X402ResponseBuilder.BuildStellarRequirements(
            payTo: "GABCDEF",
            amount: "1000000"
        );

        Assert.Equal("exact", requirements.Scheme);
        Assert.Equal("stellar:testnet", requirements.Network);
        Assert.Equal(StellarConstants.UsdcTestnet, requirements.Asset);
        Assert.Equal("1000000", requirements.Amount);
        Assert.Equal("GABCDEF", requirements.PayTo);
        Assert.Equal(60, requirements.MaxTimeoutSeconds);
        Assert.True((bool)requirements.Extra!["areFeesSponsored"]);
    }

    [Fact]
    public void BuildStellarRequirements_UsesPubnetAsset_WhenPubnetNetwork()
    {
        var requirements = X402ResponseBuilder.BuildStellarRequirements(
            payTo: "GABCDEF",
            amount: "1000000",
            network: StellarConstants.PubnetNetwork
        );

        Assert.Equal(StellarConstants.UsdcMainnet, requirements.Asset);
        Assert.Equal(StellarConstants.PubnetNetwork, requirements.Network);
    }

    [Fact]
    public void BuildPaymentRequired_SetsAllFields()
    {
        var requirements = X402ResponseBuilder.BuildStellarRequirements("GABCDEF", "100000");
        var paymentRequired = X402ResponseBuilder.BuildPaymentRequired(
            resourceUrl: "https://api.example.com/weather",
            accepts: new[] { requirements },
            description: "Weather data",
            mimeType: "application/json"
        );

        Assert.Equal(2, paymentRequired.X402Version);
        Assert.Equal("https://api.example.com/weather", paymentRequired.Resource.Url);
        Assert.Equal("Weather data", paymentRequired.Resource.Description);
        Assert.Single(paymentRequired.Accepts);
    }

    [Fact]
    public void AmountFromDecimal_ConvertsCorrectly()
    {
        Assert.Equal("100000", X402ResponseBuilder.AmountFromDecimal(0.01m));
        Assert.Equal("500000", X402ResponseBuilder.AmountFromDecimal(0.05m));
        Assert.Equal("10000000", X402ResponseBuilder.AmountFromDecimal(1.00m));
        Assert.Equal("1", X402ResponseBuilder.AmountFromDecimal(0.0000001m));
    }
}
