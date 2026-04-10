namespace X402.Stellar.Constants;

public static class StellarConstants
{
    public const string PubnetNetwork = "stellar:pubnet";
    public const string TestnetNetwork = "stellar:testnet";

    public const string UsdcMainnet = "CCW67TSZV3SSS2HXMBQ5JFGCKJNXKZM7UQUWUZPUTHXSTZLEO7SJMI75";
    public const string UsdcTestnet = "CBIELTK6YBZJU5UP2WWQEUCYKLPU6AUNZ2BQ4WWFEIE3USCIHMXQDAMA";

    public const int DefaultTokenDecimals = 7;
    public const int DefaultEstimatedLedgerCloseSeconds = 5;

    public const string DefaultTestnetRpcUrl = "https://soroban-testnet.stellar.org";
    public const string DefaultTestnetHorizonUrl = "https://horizon-testnet.stellar.org";
    public const string DefaultPubnetHorizonUrl = "https://horizon.stellar.org";
}
