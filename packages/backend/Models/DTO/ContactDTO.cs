using System.Text.Json.Serialization;

namespace backend.Models.DTO;

public class ContactDTO
{
    [JsonPropertyName("id")]
    public int? Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("phone_number")]
    public string PhoneNumber { get; set; } = string.Empty;
    [JsonPropertyName("text_comments")]
    public string TextComments { get; set; } = string.Empty;
    [JsonPropertyName("contact_type_id")]
    public int ContactTypeId { get; set; }
    [JsonPropertyName("additional_data")]
    public AdditionalDataDTO? AdditionalData {  get; set; }

}
