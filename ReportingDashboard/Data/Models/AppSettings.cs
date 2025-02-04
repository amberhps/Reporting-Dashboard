namespace ReportingDashboard.Data.Models
{
    public class AppSettings
    {
        public required string WarehouseProd { get; init; }
        public required string WarehouseTest { get; init; }
        public required string CareTendTest { get; init; }
        public required string SSIS { get; init; }
    }
}
