namespace ReportingDashboard.Data.Warehouse.Models
{
    public class MostRecentWarehouseLoad
    {
        public Guid ProcessGuid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long? LoadTime { get; set; }
        public required string LoadType { get; set; }
        public string? LoadStatus { get; set; }
        public List<WarehouseTableLoad> Tables { get; set; } = [];
    }
}
