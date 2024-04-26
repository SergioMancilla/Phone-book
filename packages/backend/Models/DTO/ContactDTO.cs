using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace backend.Models.DTO;

public class ContactDTO
{
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("phone_number")]
    public string PhoneNumber { get; set; } = string.Empty;

    [JsonPropertyName("text_comments")]
    public string TextComments { get; set; } = string.Empty;

    [Required(ErrorMessage = "Contact type ID is required")]
    [JsonPropertyName("contact_type_id")]
    public int ContactTypeId { get; set; }

    [EmailAddress(ErrorMessage = "Invalid email address")]
    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("additional_data")]
    public AdditionalDataDTO? AdditionalData { get; set; }

}
