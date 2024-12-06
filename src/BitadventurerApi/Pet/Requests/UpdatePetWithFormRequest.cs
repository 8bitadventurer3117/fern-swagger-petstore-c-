using BitadventurerApi.Core;

#nullable enable

namespace BitadventurerApi;

public record UpdatePetWithFormRequest
{
    /// <summary>
    /// Name of pet that needs to be updated
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Status of pet that needs to be updated
    /// </summary>
    public string? Status { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
