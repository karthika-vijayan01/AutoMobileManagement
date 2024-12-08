using System;
using System.Collections.Generic;

namespace AutoMobileManagement.Model;

public partial class Category
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<AutoPart> AutoParts { get; set; } = new List<AutoPart>();
}
