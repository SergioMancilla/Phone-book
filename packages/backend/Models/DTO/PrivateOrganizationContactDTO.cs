using System.Text.Json.Serialization;

namespace backend.Models.DTO;

public class PrivateOrganizationContactDTO
{
    [JsonPropertyName("office_address")]
    public string? OfficeAddress { get; set; }

    [JsonPropertyName("fax")]
    public string? Fax { get; set; }
}
