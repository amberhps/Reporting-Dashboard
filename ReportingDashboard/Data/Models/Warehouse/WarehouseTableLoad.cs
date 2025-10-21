namespace ReportingDashboard.Data.Models.Warehouse
{
    public class WarehouseTableLoad
    {
        public required string TableName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? LoadStatus { get; set; }
        public long? LoadTime { get; set; }
    }
}
