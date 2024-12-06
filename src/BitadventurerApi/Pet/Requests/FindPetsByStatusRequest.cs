using BitadventurerApi.Core;

#nullable enable

namespace BitadventurerApi;

public record FindPetsByStatusRequest
{
    /// <summary>
    /// Status values that need to be considered for filter
    /// </summary>
    public FindPetsByStatusRequestStatus? Status { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
