using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using BitadventurerApi.Core;

#nullable enable

namespace BitadventurerApi;

[JsonConverter(typeof(EnumSerializer<OrderStatus>))]
public enum OrderStatus
{
    [EnumMember(Value = "placed")]
    Placed,

    [EnumMember(Value = "approved")]
    Approved,

    [EnumMember(Value = "delivered")]
    Delivered,
}
