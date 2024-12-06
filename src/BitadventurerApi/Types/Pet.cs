using System.Text.Json.Serialization;
using BitadventurerApi.Core;

#nullable enable

namespace BitadventurerApi;

public record Pet
{
    [JsonPropertyName("id")]
    public long? Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("category")]
    public Category? Category { get; set; }

    [JsonPropertyName("photoUrls")]
    public IEnumerable<string> PhotoUrls { get; set; } = new List<string>();

    [JsonPropertyName("tags")]
    public IEnumerable<Tag>? Tags { get; set; }

    /// <summary>
    /// pet status in the store
    /// </summary>
    [JsonPropertyName("status")]
    public PetStatus? Status { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
