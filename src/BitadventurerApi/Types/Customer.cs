using System.Text.Json.Serialization;
using BitadventurerApi.Core;

#nullable enable

namespace BitadventurerApi;

public record Customer
{
    [JsonPropertyName("id")]
    public long? Id { get; set; }

    [JsonPropertyName("username")]
    public string? Username { get; set; }

    [JsonPropertyName("address")]
    public IEnumerable<Address>? Address { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
