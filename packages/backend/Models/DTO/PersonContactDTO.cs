using System.Text.Json.Serialization;

namespace backend.Models.DTO;

public class PersonContactDTO
{
    [JsonPropertyName("relationship")]
    public string? Relationship { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }
}
