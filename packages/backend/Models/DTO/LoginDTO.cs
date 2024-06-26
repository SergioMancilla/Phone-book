﻿using System.Text.Json.Serialization;

namespace backend.Models.DTO;

public class LoginDTO
{
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;
    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;
}