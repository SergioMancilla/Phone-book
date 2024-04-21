using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class Contact
{
    public Contact (string Name)
    {
        this.Name = Name;
    }

    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string? TextComments { get; set; }

    public int ContactTypeId { get; set; }

    public virtual ContactType ContactType { get; set; } = null!;

    public virtual ICollection<PersonContact> PersonContacts { get; set; } = new List<PersonContact>();

    public virtual ICollection<PrivateOrganizationContact> PrivateOrganizationContacts { get; set; } = new List<PrivateOrganizationContact>();

    public virtual ICollection<PublicOrganizationContact> PublicOrganizationContacts { get; set; } = new List<PublicOrganizationContact>();
}
