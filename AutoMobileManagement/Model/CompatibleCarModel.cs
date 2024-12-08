using System;
using System.Collections.Generic;

namespace AutoMobileManagement.Model;

public partial class CompatibleCarModel
{
    public int CompatibilityId { get; set; }

    public int? PartId { get; set; }

    public int? CarModelId { get; set; }

    public virtual CarModel? CarModel { get; set; }

    public virtual AutoPart? Part { get; set; }
}
