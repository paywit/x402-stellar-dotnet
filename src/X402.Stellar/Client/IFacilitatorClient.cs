using X402.Stellar.Types;

namespace X402.Stellar.Client;

public interface IFacilitatorClient
{
    Task<VerifyResponse> VerifyAsync(PaymentPayload paymentPayload, PaymentRequirements paymentRequirements, CancellationToken cancellationToken = default);
    Task<SettleResponse> SettleAsync(PaymentPayload paymentPayload, PaymentRequirements paymentRequirements, CancellationToken cancellationToken = default);
    Task<SupportedResponse> GetSupportedAsync(CancellationToken cancellationToken = default);
}
