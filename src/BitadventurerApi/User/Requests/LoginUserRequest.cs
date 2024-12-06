using BitadventurerApi.Core;

#nullable enable

namespace BitadventurerApi;

public record LoginUserRequest
{
    /// <summary>
    /// The user name for login
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// The password for login in clear text
    /// </summary>
    public string? Password { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
