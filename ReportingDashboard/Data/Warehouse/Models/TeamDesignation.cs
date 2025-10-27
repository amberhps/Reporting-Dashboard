namespace ReportingDashboard.Data.Warehouse.Models
{
    public class TeamDesignation
    {
        public long Id { get; set; }
        public string Team { get; set; } = "";
        public string SalesDesignation { get; set; } = "";
        public DateTime? EffectiveStartDate { get; set; }
        public DateTime? EffectiveEndDate { get; set; }
    }
}
