using System;
using System.Collections.Generic;

namespace AutoMobileManagement.Model;

public partial class CarModel
{
    public int CarModelId { get; set; }

    public string Brand { get; set; } = null!;

    public string Model { get; set; } = null!;

    public int? Year { get; set; }

    public virtual ICollection<CompatibleCarModel> CompatibleCarModels { get; set; } = new List<CompatibleCarModel>();
}
