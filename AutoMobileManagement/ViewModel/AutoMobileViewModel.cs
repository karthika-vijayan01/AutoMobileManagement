namespace AutomobileManagement.ViewModel
{
    public class AutoMobileViewModel
    {
        public string Code { get; set; } = null!;

        public string Name { get; set; } = null!;

        public int CategoryId { get; set; }

        public int CompanyId { get; set; }

        public string? Country { get; set; }

        public decimal SalePrice { get; set; }

        public string Brand { get; set; } = null!;

        public string Model { get; set; } = null!;

        public int? Year { get; set; }

    }
}
