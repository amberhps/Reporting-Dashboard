namespace ReportingDashboard.Data.Models.Warehouse
{
    public class WarehouseLoad
    {
        public Guid ProcessGuid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public required string LoadType { get; set; }
        public required string LoadStatus { get; set; }
    }
}
