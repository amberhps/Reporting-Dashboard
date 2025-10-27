namespace ReportingDashboard.Data
{
    public class AppSettings
    {
        public required string Warehouse { get; init; }
        public required string CareTend { get; init; }
        public required string SSIS { get; init; }
        public required string BackgroundJobs { get; set; }
        public required string PharmaAPI { get; set; }
    }
}
