namespace ReportingDashboard.Data.Models.PharmaAPI
{
    public class PharmaAPIConfiguration
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public required string JsonConfig { get; set; }
        public required byte RecStatus { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public required DateTime CreatedDate { get; set; }
    }
}
