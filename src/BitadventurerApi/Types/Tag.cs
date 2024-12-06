using System.Text.Json.Serialization;
using BitadventurerApi.Core;

#nullable enable

namespace BitadventurerApi;

public record Tag
{
    [JsonPropertyName("id")]
    public long? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
