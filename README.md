# x402-stellar-dotnet

The first .NET SDK for the [x402 payment protocol](https://www.x402.org/) on Stellar.

## What it does

- Parses and generates x402 payment headers (`PAYMENT-REQUIRED`, `PAYMENT-SIGNATURE`, `PAYMENT-RESPONSE`)
- Builds compliant 402 responses with payment requirements
- Facilitator HTTP client for `/verify`, `/settle`, `/supported` endpoints
- Stellar-specific constants (USDC addresses, network identifiers)
- Discovery response types for `.well-known/x402`

## Installation

```bash
dotnet add package x402-stellar-dotnet
```

## Usage

### Build a 402 response

```csharp
using X402.Stellar.Http;

var requirements = X402ResponseBuilder.BuildStellarRequirements(
    payTo: "GABCD...",
    amount: X402ResponseBuilder.AmountFromDecimal(0.01m), // $0.01 USDC
    network: "stellar:testnet"
);

var paymentRequired = X402ResponseBuilder.BuildPaymentRequired(
    resourceUrl: "https://api.example.com/weather",
    accepts: new[] { requirements },
    description: "Weather data API"
);
```

### Parse payment headers

```csharp
using X402.Stellar.Http;

var result = X402PaymentHeaderParser.Parse(headers);
if (result.HasPayment)
{
    var payload = result.PaymentPayload;
    // Verify with facilitator...
}
```

### Facilitator client

```csharp
using X402.Stellar.Client;

var client = new FacilitatorClient(new FacilitatorClientOptions
{
    Url = "https://channels.openzeppelin.com/x402/testnet",
    ApiKey = "your-api-key"
});

var verifyResult = await client.VerifyAsync(payload, requirements);
if (verifyResult.IsValid)
{
    var settleResult = await client.SettleAsync(payload, requirements);
}
```

## License

MIT
