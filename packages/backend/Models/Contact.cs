using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class Contact
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string? TextComments { get; set; }

    public int ContactTypeId { get; set; }

    public bool Deleted { get; set; }

    public virtual ContactType ContactType { get; set; } = null!;

    public virtual PersonContact? PersonContact { get; set; }

    public virtual PrivateOrganizationContact? PrivateOrganizationContact { get; set; }

    public virtual PublicOrganizationContact? PublicOrganizationContact { get; set; }
}
