namespace ReportingDashboard.Data.Jobs.Models
{
    public class MissingSSISRun
    {
        public required string JobName { get; set; }
        public required string ScheduleName { get; set; }
        public DateTime MissedRunDate { get; set; }
    }
}
