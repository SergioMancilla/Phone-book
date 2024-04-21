using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class PublicOrganizationContact
{
    public int Id { get; set; }

    public string WebpageUrl { get; set; } = null!;

    public string? IndustrialSector { get; set; }

    public int ContactId { get; set; }

    public virtual Contact Contact { get; set; } = null!;
}
