using BitadventurerApi.Core;

#nullable enable

namespace BitadventurerApi;

public record FindPetsByTagsRequest
{
    /// <summary>
    /// Tags to filter by
    /// </summary>
    public IEnumerable<string> Tags { get; set; } = new List<string>();

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
