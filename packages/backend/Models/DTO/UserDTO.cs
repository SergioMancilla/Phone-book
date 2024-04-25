using System.Text.Json.Serialization;

namespace backend.Models.DTO;

public class UserDTO
{
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;
    [JsonPropertyName("password")]
    public string? Password { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}
