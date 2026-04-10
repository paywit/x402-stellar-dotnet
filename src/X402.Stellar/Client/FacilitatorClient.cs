using System.Net.Http.Json;
using System.Text.Json;
using X402.Stellar.Constants;
using X402.Stellar.Types;

namespace X402.Stellar.Client;

public sealed class FacilitatorClientOptions
{
    public required string Url { get; set; }
    public string? ApiKey { get; set; }
}

public sealed class FacilitatorClient : IFacilitatorClient, IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly FacilitatorClientOptions _options;
    private readonly bool _ownsHttpClient;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
    };

    public FacilitatorClient(FacilitatorClientOptions options) : this(options, new HttpClient(), ownsHttpClient: true)
    {
    }

    public FacilitatorClient(FacilitatorClientOptions options, HttpClient httpClient) : this(options, httpClient, ownsHttpClient: false)
    {
    }

    private FacilitatorClient(FacilitatorClientOptions options, HttpClient httpClient, bool ownsHttpClient)
    {
        _options = options;
        _httpClient = httpClient;
        _ownsHttpClient = ownsHttpClient;

        _httpClient.BaseAddress ??= new Uri(options.Url.TrimEnd('/') + "/");

        if (!string.IsNullOrEmpty(options.ApiKey))
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", options.ApiKey);
        }
    }

    public async Task<VerifyResponse> VerifyAsync(
        PaymentPayload paymentPayload,
        PaymentRequirements paymentRequirements,
        CancellationToken cancellationToken = default)
    {
        var request = new VerifyRequest
        {
            X402Version = X402Constants.ProtocolVersion,
            PaymentPayload = paymentPayload,
            PaymentRequirements = paymentRequirements
        };

        var response = await _httpClient.PostAsJsonAsync("verify", request, JsonOptions, cancellationToken);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<VerifyResponse>(JsonOptions, cancellationToken)
               ?? throw new InvalidOperationException("Failed to deserialize verify response");
    }

    public async Task<SettleResponse> SettleAsync(
        PaymentPayload paymentPayload,
        PaymentRequirements paymentRequirements,
        CancellationToken cancellationToken = default)
    {
        var request = new SettleRequest
        {
            X402Version = X402Constants.ProtocolVersion,
            PaymentPayload = paymentPayload,
            PaymentRequirements = paymentRequirements
        };

        var response = await _httpClient.PostAsJsonAsync("settle", request, JsonOptions, cancellationToken);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<SettleResponse>(JsonOptions, cancellationToken)
               ?? throw new InvalidOperationException("Failed to deserialize settle response");
    }

    public async Task<SupportedResponse> GetSupportedAsync(CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync("supported", cancellationToken);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<SupportedResponse>(JsonOptions, cancellationToken)
               ?? throw new InvalidOperationException("Failed to deserialize supported response");
    }

    public void Dispose()
    {
        if (_ownsHttpClient)
            _httpClient.Dispose();
    }
}
