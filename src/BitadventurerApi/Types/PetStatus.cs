using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using BitadventurerApi.Core;

#nullable enable

namespace BitadventurerApi;

[JsonConverter(typeof(EnumSerializer<PetStatus>))]
public enum PetStatus
{
    [EnumMember(Value = "available")]
    Available,

    [EnumMember(Value = "pending")]
    Pending,

    [EnumMember(Value = "sold")]
    Sold,
}
