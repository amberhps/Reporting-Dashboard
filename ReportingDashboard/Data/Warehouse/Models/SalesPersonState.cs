namespace ReportingDashboard.Data.Warehouse.Models
{
    public class SalesPersonState
    {
        public long Id { get; set; }
        public long SalesPersonSalesDesignation_Id { get; set; }
        public string State { get; set; } = "";
        public DateTime? EffectiveStartDate { get; set; }
        public DateTime? EffectiveEndDate { get; set; }

        public SalesDesignation SalesPersonSalesDesignation { get; set; } = null!;
    }
}
