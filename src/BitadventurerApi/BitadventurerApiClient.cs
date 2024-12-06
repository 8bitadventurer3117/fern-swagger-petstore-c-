using BitadventurerApi.Core;

#nullable enable

namespace BitadventurerApi;

public partial class BitadventurerApiClient
{
    private RawClient _client;

    public BitadventurerApiClient(string token, string apiKey, ClientOptions? clientOptions = null)
    {
        var defaultHeaders = new Headers(
            new Dictionary<string, string>()
            {
                { "X-Fern-Language", "C#" },
                { "X-Fern-SDK-Name", "BitadventurerApi" },
                { "X-Fern-SDK-Version", Version.Current },
            }
        );
        clientOptions ??= new ClientOptions();
        foreach (var header in defaultHeaders)
        {
            if (!clientOptions.Headers.ContainsKey(header.Key))
            {
                clientOptions.Headers[header.Key] = header.Value;
            }
        }
        _client = new RawClient(clientOptions);
        Pet = new PetClient(_client);
        Store = new StoreClient(_client);
        User = new UserClient(_client);
    }

    public PetClient Pet { get; init; }

    public StoreClient Store { get; init; }

    public UserClient User { get; init; }
}
