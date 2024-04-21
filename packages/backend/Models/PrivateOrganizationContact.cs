using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class PrivateOrganizationContact
{
    public int Id { get; set; }

    public string OfficeAddress { get; set; } = null!;

    public string? Fax { get; set; }

    public int ContactId { get; set; }

    public virtual Contact Contact { get; set; } = null!;
}
