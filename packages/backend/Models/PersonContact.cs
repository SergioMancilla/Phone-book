using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class PersonContact
{
    public int Id { get; set; }

    public string? Relationship { get; set; }

    public string? Email { get; set; }

    public int ContactId { get; set; }

    public virtual Contact Contact { get; set; } = null!;
}
