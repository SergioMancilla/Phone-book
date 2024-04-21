using System.Text.Json.Serialization;

namespace backend.Models.DTO;

public class AdditionalDataDTO
{
    [JsonPropertyName("office_address")]
    public string? OfficeAddress { get; set; }

    [JsonPropertyName("fax")]
    public string? Fax { get; set; }

    [JsonPropertyName("webpage_url")]
    public string? WebpageUrl { get; set; }

    [JsonPropertyName("industrial_sector")]
    public string? IndustrialSector { get; set; }

    [JsonPropertyName("relationship")]
    public string? Relationship { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }
}
