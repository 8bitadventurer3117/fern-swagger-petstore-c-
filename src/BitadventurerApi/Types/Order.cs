using System.Text.Json.Serialization;
using BitadventurerApi.Core;

#nullable enable

namespace BitadventurerApi;

public record Order
{
    [JsonPropertyName("id")]
    public long? Id { get; set; }

    [JsonPropertyName("petId")]
    public long? PetId { get; set; }

    [JsonPropertyName("quantity")]
    public int? Quantity { get; set; }

    [JsonPropertyName("shipDate")]
    public DateTime? ShipDate { get; set; }

    /// <summary>
    /// Order Status
    /// </summary>
    [JsonPropertyName("status")]
    public OrderStatus? Status { get; set; }

    [JsonPropertyName("complete")]
    public bool? Complete { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
