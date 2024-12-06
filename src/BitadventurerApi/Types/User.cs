using System.Text.Json.Serialization;
using BitadventurerApi.Core;

#nullable enable

namespace BitadventurerApi;

public record User
{
    [JsonPropertyName("id")]
    public long? Id { get; set; }

    [JsonPropertyName("username")]
    public string? Username { get; set; }

    [JsonPropertyName("firstName")]
    public string? FirstName { get; set; }

    [JsonPropertyName("lastName")]
    public string? LastName { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("password")]
    public string? Password { get; set; }

    [JsonPropertyName("phone")]
    public string? Phone { get; set; }

    /// <summary>
    /// User Status
    /// </summary>
    [JsonPropertyName("userStatus")]
    public int? UserStatus { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
