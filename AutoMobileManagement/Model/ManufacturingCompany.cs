using System;
using System.Collections.Generic;

namespace AutoMobileManagement.Model;

public partial class ManufacturingCompany
{
    public int CompanyId { get; set; }

    public string Name { get; set; } = null!;

    public string? Country { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<AutoPart> AutoParts { get; set; } = new List<AutoPart>();
}
