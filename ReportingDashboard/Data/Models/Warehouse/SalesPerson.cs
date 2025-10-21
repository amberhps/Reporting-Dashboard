namespace ReportingDashboard.Data.Models.Warehouse
{
    public class SalesPerson
    {
        public long Id { get; set; }
        public string SalesPersonName { get; set; } = "";
        public DateTime? EffectiveStartDate { get; set; }
        public DateTime? EffectiveEndDate { get; set; }

        public ICollection<SalesDesignation> SalesDesignations { get; } = [];
    }
}
