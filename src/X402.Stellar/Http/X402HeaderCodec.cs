using System.Text;
using System.Text.Json;
using X402.Stellar.Types;

namespace X402.Stellar.Http;

public static class X402HeaderCodec
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
    };

    public static string Encode<T>(T value)
    {
        var json = JsonSerializer.Serialize(value, JsonOptions);
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
    }

    public static T Decode<T>(string base64Value)
    {
        var json = Encoding.UTF8.GetString(Convert.FromBase64String(base64Value));
        return JsonSerializer.Deserialize<T>(json, JsonOptions)
               ?? throw new InvalidOperationException($"Failed to deserialize {typeof(T).Name} from header");
    }

    public static bool TryDecode<T>(string base64Value, out T? result)
    {
        try
        {
            result = Decode<T>(base64Value);
            return result is not null;
        }
        catch
        {
            result = default;
            return false;
        }
    }

    public static string EncodePaymentRequired(PaymentRequired paymentRequired) =>
        Encode(paymentRequired);

    public static PaymentRequired DecodePaymentRequired(string headerValue) =>
        Decode<PaymentRequired>(headerValue);

    public static string EncodePaymentPayload(PaymentPayload payload) =>
        Encode(payload);

    public static PaymentPayload DecodePaymentPayload(string headerValue) =>
        Decode<PaymentPayload>(headerValue);

    public static string EncodeSettleResponse(SettleResponse response) =>
        Encode(response);

    public static SettleResponse DecodeSettleResponse(string headerValue) =>
        Decode<SettleResponse>(headerValue);
}
