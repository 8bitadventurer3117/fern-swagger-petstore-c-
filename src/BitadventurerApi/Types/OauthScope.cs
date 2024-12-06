using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using BitadventurerApi.Core;

#nullable enable

namespace BitadventurerApi;

[JsonConverter(typeof(EnumSerializer<OauthScope>))]
public enum OauthScope
{
    [EnumMember(Value = "write:pets")]
    WritePets,

    [EnumMember(Value = "read:pets")]
    ReadPets,
}
