using System;
using System.Collections.Generic;

namespace AutoMobileManagement.Model;

public partial class AutoPart
{
    public int PartId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int? CategoryId { get; set; }

    public decimal PurchasePrice { get; set; }

    public decimal SalePrice { get; set; }

    public int? CompanyId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ManufacturingCompany? Company { get; set; }

    public virtual ICollection<CompatibleCarModel> CompatibleCarModels { get; set; } = new List<CompatibleCarModel>();
}
