namespace ReportingDashboard.Data.Warehouse.Models
{
    public class TherapyTypeDesignation
    {
        public long Id { get; set; }
        public string TherapyType { get; set; } = "";
        public string SalesDesignation { get; set; } = "";
        public DateTime? EffectiveStartDate { get; set; }
        public DateTime? EffectiveEndDate { get; set; }
    }
}
