namespace ReportingDashboard.Data.Models
{
    public class ServiceAreaDesignation
    {
        public long Id { get; set; }
        public string ServiceArea { get; set; } = "";
        public string SalesDesignation { get; set; } = "";
        public DateTime? EffectiveStartDate { get; set; }
        public DateTime? EffectiveEndDate { get; set; }
    }
}
