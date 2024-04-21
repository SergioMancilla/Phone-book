using System.Text.Json.Serialization;

namespace backend.Models.DTO;

public class PublicOrganizationContactDTO
{
    [JsonPropertyName("webpage_url")]
    public string? WebpageUrl { get; set; }

    [JsonPropertyName("industrial_sector")]
    public string? IndustrialSector { get; set; }
}
